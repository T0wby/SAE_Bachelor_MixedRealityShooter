using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemyTargetDetection : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        private SphereCollider _collider;
        private List<GameObject> _placedObjs;
        private PlayerStatus _player;

        public List<GameObject> PlacedObjs => _placedObjs;
        public PlayerStatus Player => _player;

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
                _player = other.transform.parent.GetComponent<PlayerStatus>();
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
    }
}
