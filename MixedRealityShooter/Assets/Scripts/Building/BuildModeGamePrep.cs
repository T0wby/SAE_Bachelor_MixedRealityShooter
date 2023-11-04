using System.Collections;
using System.Collections.Generic;
using Items;
using Manager;
using Oculus.Interaction;
using PlacedObjects;
using Player;
using UI;
using Unity.AI.Navigation;
using UnityEngine;
using Utility;

public class BuildModeGamePrep : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MrPreparationUI _mrPreparationUI;

    private PlayerInventory _inventory;
    private GameObject _currCube;
    private GameObject _prevSelectedObj;
    private GameObject _selectedObj;
    private APlacedObject _objToDelete;

    [Header("Raycast Logic")] 
    [SerializeField] private GameObject _rightControllerVisual;

    [SerializeField] private RayInteractor _rightController;

    [Tooltip("Number of the Layer that should be hit")] 
    [SerializeField] private int _layerMaskNum = 6;

    private int _layerMask;

    [Header("Settings")] 
    [SerializeField] private float _rotPower = 1.0f;

    private int _rotationNumber = 0;
    private int _placeInvenNumber = 0;
    private Vector3 _currScale;
    private EColliderState _colliderState = EColliderState.Position;

    private bool _isBuilding = true;

    private List<GameObject> _placedObjects;

    private void Awake()
    {
        _layerMask = 1 << _layerMaskNum;
        _placedObjects = new List<GameObject>();
        _mrPreparationUI.ChangeBuildModeName(_isBuilding);
        _inventory = FindObjectOfType<PlayerInventory>();
    }

    private void FixedUpdate()
    {
        if (_isBuilding)
            SearchForPointFromInven();
        else
            SearchForPlacedInvenObjectToDelete();
    }

    private void OnEnable()
    {
        ConnectMethods();
    }

    private void OnDisable()
    {
        AddPlacedObjToOverall(GameManager.Instance.MrPlacedObjects);
        DisconnectMethods();
    }
    
    private void AddPlacedObjToOverall(List<GameObject> overallList)
    {
        foreach (var obj in _placedObjects)
        {
            if(obj == null) continue;
            if (overallList.Contains(obj)) continue;
            overallList.Add(obj);
        }
    }

    private void ConnectMethods()
    {
        // BuildMode
        _playerController.OnInteraction.AddListener(SwitchStates);
        _playerController.OnSwitchRotateScale.AddListener(SwitchRotation);
        _playerController.OnRotation.AddListener(RotateCurrCube);
        _playerController.OnPlaceObj.AddListener(AddPlacedObjectFromInven);

        // DeleteMode
        _playerController.OnPlaceObj.AddListener(DeleteFocusedObject);
    }

    private void DisconnectMethods()
    {
        // BuildMode
        _playerController.OnInteraction.RemoveListener(SwitchStates);
        _playerController.OnSwitchRotateScale.RemoveListener(SwitchRotation);
        _playerController.OnRotation.RemoveListener(RotateCurrCube);
        _playerController.OnPlaceObj.RemoveListener(AddPlacedObjectFromInven);

        // DeleteMode
        _playerController.OnPlaceObj.RemoveListener(DeleteFocusedObject);
        if(!_isBuilding && _currCube != null)
            Destroy(_currCube);
    }

    #region Raycast Logic

    private void SearchForPointFromInven()
        {
            if (_colliderState != EColliderState.Position) return;
            if (_inventory.PlaceableVRItems.Count <= 0) return;

            if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                if (_currCube == null)
                {
                    _currCube = ItemManager.Instance.ReceivePoolObject(_inventory.PlaceableVRItems[_placeInvenNumber].Type).gameObject;
                    _currCube.SetActive(true);
                }
                _currCube.transform.position = hit.point;
            }
            else
            {
                if (_currCube != null)
                    Destroy(_currCube);
            }
        }
        
        
    private void ResetPrevSelected()
        {
            if (_prevSelectedObj == null) return;
            _prevSelectedObj.GetComponent<APlacedObject>().SetNormalColor();
        }

    private void SearchForPlacedInvenObjectToDelete()
        {
            if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                if (!hit.transform.gameObject.CompareTag("InvenObj"))
                {
                    if (_objToDelete != null)
                    {
                        _objToDelete.SetNormalColor();
                        _objToDelete = null;
                    }
                    _selectedObj = null;
                    return;
                }
                _prevSelectedObj = _selectedObj;
                _selectedObj = hit.transform.gameObject;
                if (_objToDelete == null || _prevSelectedObj != _selectedObj)
                {
                    ResetPrevSelected();
                    _objToDelete = _selectedObj.GetComponent<APlacedObject>();
                    _objToDelete.SetSelectedColor();
                }
            }
            else
            {
                ResetPrevSelected();
                _prevSelectedObj = null;
                _selectedObj = null;
                _objToDelete = null;
            }
        }

    #endregion

    #region Placed Obj Action

    private void RotateCurrCube(Vector2 thumbstickValue)
    {
        if (_colliderState != EColliderState.Rotation || _currCube == null) return;

        switch (_rotationNumber)
        {
            case 0:
                // Rotate Around Y Axis
                _currCube.transform.Rotate(Vector3.up, thumbstickValue.x * _rotPower * Time.deltaTime);
                break;
            case 1:
                // Rotate Around X Axis
                _currCube.transform.Rotate(Vector3.right, thumbstickValue.x * _rotPower * Time.deltaTime);
                break;
            case 2:
                // Rotate Around Z Axis
                _currCube.transform.Rotate(Vector3.forward, thumbstickValue.x * _rotPower * Time.deltaTime);
                break;
            default:
                _currCube.transform.Rotate(Vector3.up, thumbstickValue.x * _rotPower * Time.deltaTime);
                break;
        }
    }

    private void AddPlacedObjectFromInven()
    {
        if (GameManager.Instance.CurrState != EGameStates.PreparePlayScene ||!_isBuilding) return;
        if (_currCube == null) return;

        _currCube.layer = LayerMask.NameToLayer("Environment");
        _placedObjects.Add(_currCube);
        _inventory.PlaceableVRItems.RemoveAt(_placeInvenNumber);
        _currCube = null;
    }

    private void DeleteFocusedObject()
    {
        if (_isBuilding) return;
        if (_objToDelete == null) return;
        
        var tmp = _selectedObj.GetComponent<PlaceableVRItem>();
        if (tmp != null)
        {
            _placedObjects.Remove(_selectedObj);
            _inventory.PlaceableVRItems.Add(tmp);
            tmp.Deactivate();
        }
        _objToDelete = null;
        _selectedObj = null;
    }
    #endregion

    #region Switch Modes
    
    /// <summary>
    /// Ref on Button to switch through Inventory
    /// </summary>
    public void SwitchThroughPlaceInven()
    {
        if(GameManager.Instance.CurrState != EGameStates.PreparePlayScene || !_isBuilding)return;
        if(_inventory.PlaceableVRItems.Count <= 1)return;
        _placeInvenNumber = (_placeInvenNumber + 1) % _inventory.PlaceableVRItems.Count;
        _currCube = null;
    }

    private void SwitchStates()
    {
        if (!_isBuilding) return;

        int newState = (int)(_colliderState + 1);

        _colliderState = (EColliderState)(newState % 4); // %4 due to EColliderState having 4 different states
        if (_colliderState is EColliderState.NONE or EColliderState.Scale)
            _colliderState = EColliderState.Position;
    }

    private void SwitchRotation()
    {
        if (!_isBuilding) return;
        if (_colliderState != EColliderState.Rotation) return;

        _rotationNumber = (_rotationNumber + 1) % 3;
    }

    public void SwitchBuildMode()
    {
        _isBuilding = !_isBuilding;

        _colliderState = _isBuilding ? EColliderState.Position : EColliderState.NONE;
        _mrPreparationUI.ChangeBuildModeName(_isBuilding);
        if(!_isBuilding && _currCube != null)
            Destroy(_currCube);
    }

    #endregion
}
