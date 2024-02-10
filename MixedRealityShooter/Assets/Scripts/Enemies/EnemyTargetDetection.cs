using System;
using System.Collections.Generic;
using Enemies.TeleportRangeEnemy;
using PlacedObjects;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemyTargetDetection : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AEnemy _enemy;
        private SphereCollider _collider;
        private List<GameObject> _placedObjs;
        private Collider _player;

        #endregion

        #region Properties

        public List<GameObject> PlacedObjs => _placedObjs;
        public Collider Player => _player;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _placedObjs = new List<GameObject>();
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = _enemy.Settings.SearchRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _player = other;
            else if (other.gameObject.CompareTag("PlacedObj"))
            {
                if (_placedObjs.Contains(other.gameObject))return;
                _placedObjs.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _player = null;
            else if (other.gameObject.CompareTag("PlacedObj"))
            {
                if (!_placedObjs.Contains(other.gameObject))return;
                _placedObjs.Remove(other.gameObject);
            }
        }

        #endregion

        #region Find Destination Methods
        
        public Transform GetSpawnPointTransform(bool searchUpper)
        {
            var point = searchUpper ? GetFurthestSpawnPointUpper() : GetFurthestSpawnPointLower();
            
            if (point == null)
                point = GetRandomPointAroundPlayer();

            return point;
        }

        private Transform GetFurthestSpawnPointUpper()
        {
            int count = _placedObjs.Count;

            if (count == 0) return null;
            
            int ran = Random.Range(0, count);
            var ranSelect = _placedObjs[ran].GetComponent<PlacedCube>();
            if (ranSelect == null) return null;
            Transform furthest = FurthestPoint(ranSelect.GetValidUpperSpawns());
            if (furthest == null)
                Debug.LogWarning("furthest Spawnpoint is null, which means no valid point was found");
            return furthest;
        }
        
        private Transform GetFurthestSpawnPointLower()
        {
            int count = _placedObjs.Count;

            if (count == 0) return null;
            
            int ran = Random.Range(0, count);
            var ranSelect = _placedObjs[ran].GetComponent<PlacedCube>();
            if (ranSelect == null) return null;
            Transform furthest = FurthestPoint(ranSelect.GetValidLowerSpawns());
            if (furthest == null)
                Debug.LogWarning("furthest Spawnpoint is null, which means no valid point was found");
            return furthest;
        }

        private Transform GetRandomPointAroundPlayer()
        {
            Transform targetPos = transform;
            var pos = Random.insideUnitCircle * 3;

            targetPos.position = new Vector3(pos.x, 0.5f, pos.y);

            return targetPos;
        }

        private Transform FurthestPoint(List<Transform> availableSpawns)
        {
            Transform furthest = null;
            float tmp;
            float highest = float.MinValue;

            foreach (var spawn in availableSpawns)
            {
                tmp = (spawn.position - _enemy.transform.position).sqrMagnitude;

                if (tmp > highest)
                {
                    highest = tmp;
                    furthest = spawn;
                }
            }

            return furthest;
        }

        #endregion
    }
}
