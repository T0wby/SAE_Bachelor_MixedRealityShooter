using System.Collections.Generic;
using Items;
using Manager;
using Oculus.Interaction;
using PlacedObjects;
using Player;
using UI;
using UnityEngine;
using Utility;

namespace Building
{
    public class BuildModeGamePrep : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private PlayerInventory _inventory;
        private GameObject _currCube;
        private PlaceableVRItem _currItem;
        private GameObject _prevSelectedObj;
        private GameObject _selectedObj;
        private APlacedObject _objToDelete;

        [Header("Raycast Logic")] 
        [SerializeField] private GameObject _rightControllerVisual;

        [Tooltip("Number of the Layer that should be hit")] 
        [SerializeField] private int _layerMaskNum = 6;

        private int _layerMask;

        [Header("Settings")] 
        [SerializeField] private float _rotPower = 1.0f;

        private int _rotationNumber = 0;
        private Vector3 _currScale;
        private EColliderState _colliderState = EColliderState.Position;

        private bool _isBuilding = true;

        private List<GameObject> _placedObjects;

        private void Awake()
        {
            _layerMask = 1 << _layerMaskNum;
            _placedObjects = new List<GameObject>();
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
            if(_isBuilding && _currCube != null && _currItem != null)
                ItemManager.Instance.ReturnPoolObject(_currItem);
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
            _playerController.onInteraction.AddListener(SwitchStates);
            _playerController.onThumbstickClick.AddListener(SwitchRotation);
            _playerController.OnRotation.AddListener(RotateCurrCube);
            _playerController.onPrimaryButton.AddListener(AddPlacedObjectFromInven);

            // DeleteMode
            _playerController.onPrimaryButton.AddListener(DeleteFocusedObject);
        }

        private void DisconnectMethods()
        {
            // BuildMode
            _playerController.onInteraction.RemoveListener(SwitchStates);
            _playerController.onThumbstickClick.RemoveListener(SwitchRotation);
            _playerController.OnRotation.RemoveListener(RotateCurrCube);
            _playerController.onPrimaryButton.RemoveListener(AddPlacedObjectFromInven);

            // DeleteMode
            _playerController.onPrimaryButton.RemoveListener(DeleteFocusedObject);
        }

        #region Raycast Logic

        private void SearchForPointFromInven()
        {
            if (_colliderState != EColliderState.Position || _inventory.AmountPlaceableItems <= 0) return;

            if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                if (_currCube == null) return;
                _currCube.transform.position = hit.point;
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
            _inventory.RemovePlaceableVrItem(_currItem);
            _currCube = null;
            _currItem = null;
        }

        private void DeleteFocusedObject()
        {
            if (_isBuilding) return;
            if (_objToDelete == null) return;
        
            var tmp = _selectedObj.GetComponent<PlaceableVRItem>();
            if (tmp != null)
            {
                _placedObjects.Remove(_selectedObj);
                _inventory.AddPlaceableVrItem(tmp);
                _selectedObj.layer = LayerMask.NameToLayer("Default");
                _selectedObj.SetActive(false);
            }
            _objToDelete = null;
            _selectedObj = null;
        }
        #endregion

        #region Switch Modes
        
        public void SetCurrItem(PlaceableVRItem newItem)
        {
            if(GameManager.Instance.CurrState != EGameStates.PreparePlayScene || !_isBuilding)return;

            if (_currCube != null)
                _currCube.SetActive(false);
            _currItem = newItem;
            _currCube = newItem.gameObject;
            _currCube.SetActive(true);
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

        public void SwitchBuildMode(bool isOn)
        {
            _isBuilding = isOn;

            _colliderState = _isBuilding ? EColliderState.Position : EColliderState.NONE;
            if(!_isBuilding && _currCube != null && _currItem != null)
                _currCube.SetActive(false);
        }

        #endregion
    }
}
