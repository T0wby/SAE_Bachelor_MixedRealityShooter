using System.Collections.Generic;
using Manager;
using Oculus.Interaction;
using UnityEngine;
using Utility;
using Player;
using UI;
using UnityEngine.Serialization;

public class MRPreperationBuilder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private MrPreparationUI _mrPreparationUI;

    private GameObject _currCube;
    private GameObject _objToDelete;

    [Header("Raycast Logic")]
    [SerializeField] private GameObject _rightControllerVisual;
    [SerializeField] private RayInteractor _rightController;
    [Tooltip("Number of the Layer that should be hit")]
    [SerializeField] private int _layerMaskNum = 6;
    private int _layerMask;
    
    [Header("Settings")] 
    [SerializeField] private float _rotPower = 1.0f;
    [SerializeField] private float _scalePower = 1.0f;
    
    private int _scaleNumber = 0;
    private int _rotationNumber = 0;
    private Vector3 _currScale;
    private EColliderState _colliderState = EColliderState.Position; 
    private bool _isBuilding = true;
    private List<GameObject> _placedObjects;

    private void Awake()
    {
        _layerMask = 1 << _layerMaskNum;
        _placedObjects = new List<GameObject>();
        _mrPreparationUI.ChangeBuildModeName(_isBuilding);
    }

    private void Start()
    {
        ConnectMethods();
    }

    private void FixedUpdate()
    {
        if (_isBuilding)
            SearchForPoint();
        else
            SearchForObject();
    }

    private void ConnectMethods()
    {
        GameManager.Instance.OnGameStateChange.AddListener(CheckGameState);
        
        // BuildMode
        _playerController.OnInteraction.AddListener(SwitchStates);
        _playerController.OnSwitchRotateScale.AddListener(SwitchRotation);
        _playerController.OnSwitchRotateScale.AddListener(SwitchScaling);
        _playerController.OnRotation.AddListener(RotateCurrCube);
        _playerController.OnScale.AddListener(ScaleCurrCube);
        _playerController.OnPlaceObj.AddListener(AddPlacedObject);
        
        // DeleteMode
        _playerController.OnPlaceObj.AddListener(DeleteFocusedObject);
    }

    private void CheckGameState()
    {
        _isBuilding = GameManager.Instance.CurrState == EGameStates.PrepareMRScene;
    }

    /// <summary>
    /// Uses a Raytrace to search for placed Objects and sets a reference if one is hit
    /// </summary>
    private void SearchForObject()
    {
        if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward, out var hit, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(_rightControllerVisual.transform.position,_rightControllerVisual.transform.forward * hit.distance, Color.green);
            if (!hit.transform.gameObject.CompareTag("PlacedObj")) return;
            _objToDelete = hit.transform.gameObject;
        }
        else
        {
            Debug.DrawRay(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward * 1000, Color.red);
            _objToDelete = null;
        }
    }
    
    /// <summary>
    /// Uses a Raytrace to find a point in the Environment, to spawn and place a new Object
    /// </summary>
    private void SearchForPoint()
    {
        if (_colliderState != EColliderState.Position)return;
        
        if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward, out var hit, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward * hit.distance, Color.green);
            if (_currCube == null)
                _currCube = Instantiate(_cubePrefab, hit.transform.position, Quaternion.identity);
            _currCube.transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward * 1000, Color.red);
            if (_currCube != null)
                Destroy(_currCube);
        }
    }

    private void RotateCurrCube(Vector2 thumbstickValue)
    {
        if (_colliderState != EColliderState.Rotation || _currCube == null)return;

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
    
    private void ScaleCurrCube(Vector2 thumbstickValue)
    {
        if (_colliderState != EColliderState.Scale || _currCube == null)return;

        _currScale = _currCube.transform.localScale;
        
        switch (_scaleNumber)
        {
            case 0:
                // Scale the Y Axis
                _currScale.y += thumbstickValue.y * _scalePower * Time.deltaTime;
                break;
            case 1:
                // Scale the X Axis
                _currScale.x += thumbstickValue.y * _scalePower * Time.deltaTime;
                break;
            case 2:
                // Scale the Z Axis
                _currScale.z += thumbstickValue.y * _scalePower * Time.deltaTime;
                break;
            default:
                _currScale.y += thumbstickValue.y * _scalePower * Time.deltaTime;
                break;
        }

        _currCube.transform.localScale = _currScale;
    }

    private void SwitchStates()
    {
        if (!_isBuilding)return;
        
        int newState = (int)(_colliderState + 1);
        
        _colliderState = (EColliderState)(newState % 4); // %4 due to EColliderState having 4 different states
        if (_colliderState == EColliderState.NONE)
            _colliderState = EColliderState.Position;
    }
    private void SwitchRotation()
    {
        if (!_isBuilding)return;
        if (_colliderState != EColliderState.Rotation)return;

        _rotationNumber = (_rotationNumber+1) % 3;
    }
    private void SwitchScaling()
    {
        if (!_isBuilding)return;
        if (_colliderState != EColliderState.Scale)return;

        _scaleNumber = (_scaleNumber+1) % 3;
    }

    public void SwitchBuildMode()
    {
        _isBuilding = !_isBuilding;

        _colliderState = _isBuilding ? EColliderState.Position : EColliderState.NONE;
        _mrPreparationUI.ChangeBuildModeName(_isBuilding);
    }

    private void AddPlacedObject()
    {
        if (!_isBuilding) return;
        if (_currCube == null) return;

        _currCube.layer = LayerMask.NameToLayer("Environment");
        _currCube.transform.GetChild(0).transform.gameObject.layer = LayerMask.NameToLayer("Environment"); // Currently only holds one children that has the collision component
        _placedObjects.Add(_currCube);
        _currCube = null;
    }
    
    private void DeleteFocusedObject()
    {
        if (_isBuilding) return;
        if (_objToDelete == null) return;

        _placedObjects.Remove(_objToDelete);
        Destroy(_objToDelete);
        _objToDelete = null;
    }
}
