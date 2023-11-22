using System;
using System.Collections.Generic;
using Manager;
using Oculus.Interaction;
using PlacedObjects;
using UI;
using UnityEngine;
using Utility;
using Items;
using Player;
using Unity.AI.Navigation;

namespace Building
{
    public class BuildModeWall : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MrPreparationUI _mrPreparationUI;

        private GameObject _currWall;
        private GameObject _prevSelectedObj;
        private GameObject _selectedObj;
        private APlacedObject _objToDelete;

        [Header("Raycast Logic")] [SerializeField]
        private GameObject _rightControllerVisual;

        [SerializeField] private RayInteractor _rightController;

        [Tooltip("Number of the Layer that should be hit")] [SerializeField]
        private int _layerMaskNum = 6;

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

        private void FixedUpdate()
        {
            if (_isBuilding)
                SearchForPoint();
            else
                SearchForObject();
        }

        private void OnEnable()
        {
            ConnectMethods();
        }

        private void OnDisable()
        {
            AddPlacedObjToOverall(GameManager.Instance.MrPlacedObjects);
            DisconnectMethods();
            if(_isBuilding && _currWall != null)
                Destroy(_currWall);
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
            _playerController.onThumbstickClick.AddListener(SwitchScaling);
            _playerController.OnRotation.AddListener(RotateCurrCube);
            _playerController.OnScale.AddListener(ScaleCurrCube);
            _playerController.onPrimaryButton.AddListener(AddPlacedObject);

            // DeleteMode
            _playerController.onPrimaryButton.AddListener(DeleteFocusedObject);
        }

        private void DisconnectMethods()
        {
            // BuildMode
            _playerController.onInteraction.RemoveListener(SwitchStates);
            _playerController.onThumbstickClick.RemoveListener(SwitchRotation);
            _playerController.onThumbstickClick.RemoveListener(SwitchScaling);
            _playerController.OnRotation.RemoveListener(RotateCurrCube);
            _playerController.OnScale.RemoveListener(ScaleCurrCube);
            _playerController.onPrimaryButton.RemoveListener(AddPlacedObject);

            // DeleteMode
            _playerController.onPrimaryButton.RemoveListener(DeleteFocusedObject);
        }

        #region Raycast Logic

        /// <summary>
        /// Uses a Raytrace to search for placed Objects and sets a reference if one is hit
        /// </summary>
        private void SearchForObject()
        {
            if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                if (!hit.transform.gameObject.CompareTag("Wall"))
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

        private void ResetPrevSelected()
        {
            if (_prevSelectedObj == null) return;
            _prevSelectedObj.GetComponent<APlacedObject>().SetNormalColor();
        }

        /// <summary>
        /// Uses a Raytrace to find a point in the Environment, to spawn and place a new Object
        /// </summary>
        private void SearchForPoint()
        {
            if (_colliderState != EColliderState.Position) return;

            if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                if (_currWall == null)
                    _currWall = Instantiate(_wallPrefab, hit.transform.position, Quaternion.identity);
                _currWall.transform.position = hit.point;
            }
            else
            {
                if (_currWall != null)
                    Destroy(_currWall);
            }
        }
        
        #endregion

        #region Object Interactions

        private void RotateCurrCube(Vector2 thumbstickValue)
        {
            if (_colliderState != EColliderState.Rotation || _currWall == null) return;

            switch (_rotationNumber)
            {
                case 0:
                    // Rotate Around Y Axis
                    _currWall.transform.Rotate(Vector3.up, thumbstickValue.x * _rotPower * Time.deltaTime);
                    break;
                case 1:
                    // Rotate Around X Axis
                    _currWall.transform.Rotate(Vector3.right, thumbstickValue.x * _rotPower * Time.deltaTime);
                    break;
                case 2:
                    // Rotate Around Z Axis
                    _currWall.transform.Rotate(Vector3.forward, thumbstickValue.x * _rotPower * Time.deltaTime);
                    break;
                default:
                    _currWall.transform.Rotate(Vector3.up, thumbstickValue.x * _rotPower * Time.deltaTime);
                    break;
            }
        }

        private void ScaleCurrCube(Vector2 thumbstickValue)
        {
            if (_colliderState != EColliderState.Scale || _currWall == null) return;

            _currScale = _currWall.transform.localScale;

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

            _currWall.transform.localScale = _currScale;
        }
        
        private void AddPlacedObject()
        {
            if (GameManager.Instance.CurrState != EGameStates.PrepareMRSceneWall || !_isBuilding) return;
            if (_currWall == null) return;

            _currWall.layer = LayerMask.NameToLayer("Environment");
            _currWall.transform.GetChild(0).transform.gameObject.layer =
                LayerMask.NameToLayer("Environment"); // Currently only holds one children that has the collision component
            _placedObjects.Add(_currWall);
            _currWall = null;
            //TODO: Add Spatial Anchor, save it locally and save UUID separately
        }
        
        private void DeleteFocusedObject()
        {
            if (_isBuilding) return;
            if (_objToDelete == null) return;

            _placedObjects.Remove(_selectedObj);
            Destroy(_selectedObj);
            _objToDelete = null;
            _selectedObj = null;
            //TODO: Delete Spatial Anchor, delete it locally and delete UUID
        }

        #endregion

        #region SwitchModes

        private void SwitchStates()
        {
            if (!_isBuilding) return;

            int newState = (int)(_colliderState + 1);

            _colliderState = (EColliderState)(newState % 4); // %4 due to EColliderState having 4 different states
            if (_colliderState == EColliderState.NONE)
                _colliderState = EColliderState.Position;
        }

        private void SwitchRotation()
        {
            if (!_isBuilding) return;
            if (_colliderState != EColliderState.Rotation) return;

            _rotationNumber = (_rotationNumber + 1) % 3;
        }

        private void SwitchScaling()
        {
            if (!_isBuilding) return;
            if (_colliderState != EColliderState.Scale) return;

            _scaleNumber = (_scaleNumber + 1) % 3;
        }

        public void SwitchBuildMode()
        {
            _isBuilding = !_isBuilding;

            _colliderState = _isBuilding ? EColliderState.Position : EColliderState.NONE;
            _mrPreparationUI.ChangeBuildModeName(_isBuilding);
            if(!_isBuilding && _currWall != null)
                Destroy(_currWall);
        }

        #endregion
    }
}