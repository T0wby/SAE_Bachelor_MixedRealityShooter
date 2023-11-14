using System.Collections.Generic;
using System.Linq;
using Manager;
using Oculus.Interaction;
using PlacedObjects;
using Player;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Building
{
    public class BuildModeWallThreePoint : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MrPreparationUI _mrPreparationUI;
        //[SerializeField] private LineRenderer _lineRenderer;

        private GameObject _currWall;
        private GameObject _prevSelectedObj;
        private GameObject _selectedObj;
        private APlacedObject _objToDelete;

        [Header("Raycast Logic")] 
        [SerializeField] private GameObject _rightControllerBuildPoint;

        [Tooltip("Number of the Layer that should be hit")] [SerializeField]
        private int _layerMaskNum = 6;

        private int _layerMask;
        
        [Header("NewPlaceLogic")] 
        [SerializeField] private GameObject _placedPointPrefab;
        [SerializeField] private GameObject _heightWallPrefab;
        [SerializeField] private Transform _modeParent;
        private GameObject _startPoint;
        private GameObject _heightPoint;
        private GameObject _secondPoint;
        private GameObject _heightWall;
        private GameObject _followPoint;
        private Vector3 _secondPos;
        private Vector3 _origin;
        private Vector3 _scale;
        private Vector3 _currPoint;
        private bool _isBuilding = true;
        private List<GameObject> _placedObjects;
        private EPlaceModeWall _currPlaceMode = EPlaceModeWall.Start;
        
        private enum EPlaceModeWall
        {
            None,
            Start,
            SecondPoint,
            Height
        }

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
            _followPoint = Instantiate(_placedPointPrefab, Vector3.down, Quaternion.identity, _modeParent);
            //_lineRenderer.SetPosition(0, _rightControllerBuildPoint.transform.position);
        }

        private void OnDisable()
        {
            AddPlacedObjToOverall(GameManager.Instance.MrPlacedObjects);
            DisconnectMethods();
            if(_isBuilding && _currWall != null)
                Destroy(_currWall);
            Destroy(_followPoint);
        }

        private void AddPlacedObjToOverall(List<GameObject> overallList)
        {
            if (overallList == null)return;
            foreach (var obj in _placedObjects.Where(obj => obj != null).Where(obj => !overallList.Contains(obj)))
            {
                overallList.Add(obj);
            }
        }
        
        private void ConnectMethods()
        {
            // BuildMode
            _playerController.onPrimaryButton.AddListener(AddReferenceGameObject);

            // DeleteMode
            _playerController.onPrimaryButton.AddListener(DeleteFocusedObject);
        }

        private void DisconnectMethods()
        {
            // BuildMode
            _playerController.onPrimaryButton.RemoveListener(AddReferenceGameObject);

            // DeleteMode
            _playerController.onPrimaryButton.RemoveListener(DeleteFocusedObject);
        }

        #region Raycast Logic

        /// <summary>
        /// Uses a Raytrace to search for placed Objects and sets a reference if one is hit
        /// </summary>
        private void SearchForObject()
        {
            if (Physics.Raycast(_rightControllerBuildPoint.transform.position, _rightControllerBuildPoint.transform.forward,
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
            if (_currPlaceMode == EPlaceModeWall.None) return;

            if (Physics.Raycast(_rightControllerBuildPoint.transform.position, _rightControllerBuildPoint.transform.forward,
                    out var hit, Mathf.Infinity, _layerMask))
            {
                _currPoint = hit.point;
                _followPoint.transform.position = hit.point;
            }
        }
        
        #endregion

        #region Object Interactions
        
        private void AddReferenceGameObject()
        {
            switch (_currPlaceMode)
            {
                case EPlaceModeWall.Start:
                    _startPoint = Instantiate(_placedPointPrefab, _currPoint, Quaternion.identity, _modeParent);
                    break;
                case EPlaceModeWall.SecondPoint:
                    _secondPoint = Instantiate(_placedPointPrefab, _currPoint, Quaternion.identity, _modeParent);
                    _secondPos = _secondPoint.transform.position;
                    _heightWall = Instantiate(_heightWallPrefab, _currPoint, Quaternion.Euler(0, 90, 0), _modeParent);
                    UtilityMethods.CalcQuadTransform(ref _heightWall, _startPoint.transform.position, _secondPos, 
                        new Vector3(_secondPos.x, 1.0f, _secondPos.z), 20.0f);
                    break;
                case EPlaceModeWall.Height:
                    _heightPoint = Instantiate(_placedPointPrefab, new Vector3(_secondPos.x, _currPoint.y, _secondPos.z),
                        Quaternion.identity, _modeParent);
                    // Calculate origin, scaling and rotation?
                    _currWall = Instantiate(_wallPrefab, Vector3.down * 20, Quaternion.identity);
                    UtilityMethods.CalcQuadTransform(ref _currWall, _startPoint.transform.position, _secondPos, 
                        _heightPoint.transform.position);
                    AddPlacedObject();
                    Destroy(_heightWall);
                    break;
                default:
                    return;
            }
            SwitchStates();
        }
       
        private void AddPlacedObject()
        {
            if (GameManager.Instance.CurrState != EGameStates.PrepareMRSceneWall || !_isBuilding) return;
            if (_currWall == null) return;

            _currWall.layer = LayerMask.NameToLayer("Environment");
            _placedObjects.Add(_currWall);
            _currWall = null;
            _startPoint = null;
            _heightPoint = null;
            _secondPoint = null;
        }
        
        private void DeleteFocusedObject()
        {
            if (_isBuilding) return;
            if (_objToDelete == null) return;

            _placedObjects.Remove(_selectedObj);
            Destroy(_selectedObj);
            _objToDelete = null;
            _selectedObj = null;
        }

        #endregion

        #region SwitchModes

        private void SwitchStates()
        {
            if (!_isBuilding) return;

            int newState = (int)(_currPlaceMode + 1);

            _currPlaceMode = (EPlaceModeWall)(newState % 4); // %4 due to EColliderState having 4 different states
            if (_currPlaceMode == EPlaceModeWall.None)
                _currPlaceMode = EPlaceModeWall.Start;
        }

        public void SwitchBuildMode()
        {
            _isBuilding = !_isBuilding;

            _currPlaceMode = _isBuilding ? EPlaceModeWall.Start : EPlaceModeWall.None;
            _mrPreparationUI.ChangeBuildModeName(_isBuilding);
            if(!_isBuilding && _currWall != null)
                Destroy(_currWall);
        }

        #endregion
    }
}
