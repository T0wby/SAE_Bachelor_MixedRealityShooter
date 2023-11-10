using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace PlacedObjects
{
    public class PlacedCube : APlacedObject
    {
        [SerializeField] private List<Transform> _aiSpawns;
        [SerializeField] private GameObject _startObj;
        [SerializeField] private GameObject _heightObj;
        [SerializeField] private GameObject _endObj;
        private GameObject _self;
        private bool _canStartTransformCheck = false;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private float _heightY;


        private void Start()
        {
            _self = gameObject;
        }
        
        private void LateUpdate()
        {
            TransformUpdates();
        }

        /// <summary>
        /// Returns a List of spawn points that are not obstructed by a collider
        /// </summary>
        /// <returns>List of Transforms</returns>
        public List<Transform> GetValidSpawnPoints()
        {
            List<Transform> valid = new List<Transform>();
            Collider[] hitColliders = new Collider[20];
            bool hasCollision = false;

            foreach (var spawn in _aiSpawns)
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

        public void SetTransformPoints(GameObject startObj, GameObject heightObj, GameObject endObj)
        {
            _startObj = startObj;
            _heightObj = heightObj;
            _endObj = endObj;
            _canStartTransformCheck = true;
        }

        private void TransformUpdates()
        {
            if (!_canStartTransformCheck)return;
            _heightY = _heightObj.transform.position.y;
            _startPos = _startObj.transform.position;
            _endPos = _endObj.transform.position;

            _heightObj.transform.position = new Vector3(_startPos.x, _heightY, _startPos.z);
            _endPos = new Vector3(_endPos.x, _heightY, _endPos.z);
            
            UtilityMethods.CalcBoxTransform(ref _self, _startPos, _heightY, _endPos);
        }
    }
}
