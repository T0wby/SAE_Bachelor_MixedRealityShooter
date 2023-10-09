using Manager;
using Oculus.Interaction;
using UnityEngine;
using Utility;
using Player;
using UnityEngine.Serialization;

public class MRPreperationBuilder : MonoBehaviour
{
    [Header("Referenzes")]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private PlayerController _playerController;

    private GameObject _currCube;
    

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

    private void Awake()
    {
        _layerMask = 1 << _layerMaskNum;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange.AddListener(CheckGameState);
        _playerController.OnInteraction.AddListener(SwitchStates);
        _playerController.OnSwitchRotateScale.AddListener(SwitchRotation);
        _playerController.OnSwitchRotateScale.AddListener(SwitchScaling);
        _playerController.OnRotation.AddListener(RotateCurrCube);
        _playerController.OnScale.AddListener(ScaleCurrCube);
    }

    private void FixedUpdate()
    {
        if (!_isBuilding)return;
    
        SearchForPoint();
        
    }

    private void CheckGameState()
    {
        _isBuilding = GameManager.Instance.CurrState == EGameStates.PrepareMRScene;
    }

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
        int newState = (int)(_colliderState + 1);
        _colliderState = (EColliderState)(newState % 3); // %3 due to EColliderState having 3 different states
    }
    private void SwitchRotation()
    {
        if (_colliderState != EColliderState.Rotation)return;

        _rotationNumber = (_rotationNumber+1) % 3;
    }
    private void SwitchScaling()
    {
        if (_colliderState != EColliderState.Scale)return;

        _scaleNumber = (_scaleNumber+1) % 3;
    }
}
