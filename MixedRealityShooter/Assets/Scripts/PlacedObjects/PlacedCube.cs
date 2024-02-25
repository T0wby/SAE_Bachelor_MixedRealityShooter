using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace PlacedObjects
{
    public class PlacedCube : APlacedObject
    {
        [SerializeField] private List<Transform> _aiSpawnsUpper;
        [SerializeField] private List<Transform> _aiSpawnsLower;
        private GameObject _startObj;
        private GameObject _heightObj;
        private GameObject _endObj;
        private GameObject _widthObj;
        private GameObject _self;
        private GameObject _parent;
        private Vector3 _startPos;
        private Vector3 _widthPos;
        private Vector3 _endPos;
        private float _heightY;
        private bool _canStartTransformCheck = false;


        private void Start()
        {
            _self = gameObject;
            if (_self.transform.parent != null)
             _parent = _self.transform.parent.gameObject;
        }
        
        private void LateUpdate()
        {
            TransformUpdates();
        }

        public List<Transform> GetValidUpperSpawns()
        {
            return GetValidSpawnPoints(_aiSpawnsUpper);
        }
        
        public List<Transform> GetValidLowerSpawns()
        {
            return GetValidSpawnPoints(_aiSpawnsLower);
        }

        /// <summary>
        /// Returns a List of spawn points that are not obstructed by a collider
        /// </summary>
        /// <returns>List of Transforms</returns>
        private List<Transform> GetValidSpawnPoints(List<Transform> aiSpawns)
        {
            List<Transform> valid = new List<Transform>();
            Collider[] hitColliders = new Collider[20];
            bool hasCollision = false;

            foreach (var spawn in aiSpawns)
            {
                Physics.OverlapSphereNonAlloc(spawn.position, 0.0f, hitColliders);
                foreach (var coll in hitColliders)
                {
                    if (coll == null || !coll.isTrigger)
                        continue;
                    hasCollision = true;
                    break;
                }

                if (!hasCollision)
                    valid.Add(spawn);
                else
                    hasCollision = false;
            }
            
            return valid;
        }

        public void SetTransformPoints(GameObject startObj, GameObject widthObj, GameObject heightObj, GameObject endObj)
        {
            _startObj = startObj;
            _widthObj = widthObj;
            _heightObj = heightObj;
            _endObj = endObj;
            _canStartTransformCheck = true;
        }

        private void OnDisable()
        {
            _canStartTransformCheck = false;
        }

        public void DisableTransformChange()
        {
            _canStartTransformCheck = false;
        }

        private void TransformUpdates()
        {
            if (!_canStartTransformCheck)return;
            if (_heightObj == null)return;
            _heightY = _heightObj.transform.position.y;
            if (_startObj == null)return;
            _startPos = _startObj.transform.position;
            if (_widthObj == null)return;
            _widthPos = _widthObj.transform.position;
            if (_endObj == null)return;
            _endPos = _endObj.transform.position;

            _widthObj.transform.position = new Vector3(_widthPos.x, _startPos.y, _widthPos.z);
            _heightObj.transform.position = new Vector3(_widthPos.x, _heightY, _widthPos.z);
            _endObj.transform.position = new Vector3(_endPos.x, _heightY, _endPos.z);

            if (_parent == null)
                UtilityMethods.CalcBoxTransform(ref _self, _startPos, _widthPos, _heightObj.transform.position, _endObj.transform.position);
            else
                UtilityMethods.CalcBoxTransform(ref _self, ref _parent, _startPos, _widthPos, _heightObj.transform.position, _endObj.transform.position);
        }

        public void DeleteTransformPoints()
        {
            if (_heightObj != null)
                Destroy(_heightObj);
            if (_startObj != null)
                Destroy(_startObj);
            if (_widthObj != null)
                Destroy(_widthObj);
            if (_endObj != null)
                Destroy(_endObj);
        }
    }
}
