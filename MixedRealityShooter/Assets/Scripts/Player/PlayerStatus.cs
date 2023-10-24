using System;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private int _health = 100;
        [SerializeField] private Transform _centerEyeAnchor;
        [SerializeField] private CapsuleCollider _thisCollider;

        private GameObject _colliderGO;
        
        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnHealthChange.Invoke(_health);
            }
        }

        public UnityEvent<int> OnHealthChange;

        private void Awake()
        {
            if(_thisCollider == null)return;
            _colliderGO = _thisCollider.gameObject;
        }

        private void Update()
        {
            if(_centerEyeAnchor == null) return;
            _thisCollider.height = _centerEyeAnchor.position.y;
            _colliderGO.transform.position = new Vector3(_centerEyeAnchor.position.x, _thisCollider.height * 0.5f,
                _centerEyeAnchor.position.z);
        }
    }
}
