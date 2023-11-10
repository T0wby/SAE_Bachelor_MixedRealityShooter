using System;
using System.Collections.Generic;
using Manager;
using PlacedObjects;
using Player;
using UI;
using UnityEngine;
using Utility;

namespace Building
{
    public class BuildModeInnerHandPlacement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MrPreparationUI _mrPreparationUI;

        private GameObject _currCube;
        private GameObject _prevSelectedObj;
        private GameObject _selectedObj;
        private APlacedObject _objToDelete;
        private bool _isBuilding = true;
        private List<GameObject> _placedObjects;
        
        [Header("Raycast Logic")] [SerializeField]
        private GameObject _rightControllerVisual;
        private Vector3 _controllerPos;

        [Tooltip("Number of the Layer that should be hit")] 
        [SerializeField] private int _layerMaskNum = 6;

        private int _layerMask;

        [Header("NewPlaceLogic")] 
        [SerializeField] private GameObject _placedPointPrefab;
        private GameObject _startPoint;
        private GameObject _heightPoint;
        private GameObject _endPoint;
        private Vector3 _origin;
        private Vector3 _scale;
        private Vector2 _startXZ;
        private float _heightY;
        private Vector3 _currPoint;
        private EPlaceMode _currPlaceMode = EPlaceMode.Start;

        private enum EPlaceMode
        {
            None,
            Start,
            Height,
            Scale
        }
        

        private void Awake()
        {
            _layerMask = 1 << _layerMaskNum;
            _placedObjects = new List<GameObject>();
            if(_mrPreparationUI != null)
                _mrPreparationUI.ChangeBuildModeName(_isBuilding);
        }

        private void FixedUpdate()
        {
            if (_isBuilding)
                CalculateTrackingPosition();
            else
                SearchForObject();
        }

        private void OnEnable()
        {
            ConnectMethods();
        }

        private void OnDisable()
        {
            DisconnectMethods();
            if(_isBuilding && _currCube != null)
                Destroy(_currCube);
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
        private void RemovePlacedObjFromOverall(List<GameObject> overallList, GameObject objToRemove)
        {
            if (overallList.Contains(objToRemove))
                overallList.Remove(objToRemove);
        }

        private void ConnectMethods()
        {
            // BuildMode
            _playerController.OnInteraction.AddListener(AddReferenceGameObject);

            // DeleteMode
            _playerController.OnPlaceObj.AddListener(DeleteFocusedObject);
        }

        private void DisconnectMethods()
        {
            // BuildMode
            _playerController.OnInteraction.RemoveListener(AddReferenceGameObject);

            // DeleteMode
            _playerController.OnPlaceObj.RemoveListener(DeleteFocusedObject);
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
                if (!hit.transform.gameObject.CompareTag("PlacedObj"))
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
        /// 
        /// </summary>
        private void CalculateTrackingPosition()
        {
            _controllerPos = _rightControllerVisual.transform.position;
            switch (_currPlaceMode)
            {
                case EPlaceMode.Start:
                    _currPoint = _controllerPos;
                    break;
                case EPlaceMode.Height:
                    if (_startPoint == null)return;
                    _currPoint = new Vector3(_startXZ.x, _controllerPos.y, _startXZ.y);
                    break;
                case EPlaceMode.Scale:
                    if (_heightPoint == null)return;
                    _currPoint = new Vector3(_controllerPos.x, _heightY, _controllerPos.z);
                    break;
                default:
                    return;
            }
        }

        #endregion

        #region Placed Obj Action

        private void AddReferenceGameObject()
        {
            switch (_currPlaceMode)
            {
                case EPlaceMode.Start:
                    _startPoint = Instantiate(_placedPointPrefab, _currPoint, Quaternion.identity);
                    _startXZ = new Vector2(_startPoint.transform.position.x, _startPoint.transform.position.z);
                    break;
                case EPlaceMode.Height:
                    _heightPoint = Instantiate(_placedPointPrefab, _currPoint, Quaternion.identity);
                    _heightY = _heightPoint.transform.position.y;
                    break;
                case EPlaceMode.Scale:
                    _endPoint = Instantiate(_placedPointPrefab, _currPoint, Quaternion.identity);
                    // Calculate origin, scaling and place cube prefab
                    _currCube = Instantiate(_cubePrefab, Vector3.down * 20, Quaternion.identity);
                    var tmp = _currCube.GetComponent<PlacedCube>();
                    if (tmp != null)
                    {
                        tmp.SetTransformPoints(_startPoint, _heightPoint, _endPoint);
                    }
                        
                    UtilityMethods.CalcBoxTransform(ref _currCube, _startPoint.transform.position, _heightY, _endPoint.transform.position);
                    //CalcBoxTransform(_startPoint.transform.position, _heightY, _endPoint.transform.position);
                    AddPlacedObject();
                    break;
                default:
                    return;
            }
            SwitchStates();
        }

        private void CalcBoxTransform(Vector3 startPos, float heightPos, Vector3 endPos)
        {
            _origin = (startPos + endPos) * 0.5f;
            _scale = new Vector3(Math.Abs(endPos.x - startPos.x), Math.Abs(heightPos - startPos.y),
                Math.Abs(endPos.z - startPos.z));

            _currCube = Instantiate(_cubePrefab, _origin, Quaternion.identity);
            _currCube.transform.localScale = _scale;
            
        }

        private void AddPlacedObject()
        {
            if (GameManager.Instance.CurrState != EGameStates.PrepareMRSceneInner || !_isBuilding) return;
            if (_currCube == null) return;

            _currCube.layer = LayerMask.NameToLayer("Environment");
            _currCube.transform.GetChild(0).transform.gameObject.layer =
                LayerMask.NameToLayer("Environment"); // Currently only holds one children that has the collision component
            _placedObjects.Add(_currCube);
            AddPlacedObjToOverall(GameManager.Instance.MrPlacedObjects);
            _currCube = null;
            _startPoint = null;
            _heightPoint = null;
            _endPoint = null;
        }

        private void DeleteFocusedObject()
        {
            if (_isBuilding) return;
            if (_objToDelete == null) return;

            _placedObjects.Remove(_selectedObj);
            RemovePlacedObjFromOverall(GameManager.Instance.MrPlacedObjects, _selectedObj);
            Destroy(_selectedObj);
            _objToDelete = null;
            _selectedObj = null;
        }
        #endregion

        #region Switch Modes

        private void SwitchStates()
        {
            if (!_isBuilding) return;

            int newState = (int)(_currPlaceMode + 1);

            _currPlaceMode = (EPlaceMode)(newState % 4); // %4 due to EColliderState having 4 different states
            if (_currPlaceMode == EPlaceMode.None)
                _currPlaceMode = EPlaceMode.Start;
        }

        public void SwitchBuildMode()
        {
            _isBuilding = !_isBuilding;

            _currPlaceMode = _isBuilding ? EPlaceMode.Start : EPlaceMode.None;
            _mrPreparationUI.ChangeBuildModeName(_isBuilding);
            if(!_isBuilding && _currCube != null)
                Destroy(_currCube);
        }

        #endregion
    }
}
