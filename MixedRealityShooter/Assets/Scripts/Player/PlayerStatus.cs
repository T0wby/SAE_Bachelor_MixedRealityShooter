using System;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _health = MAXHEALTH;
        [SerializeField] private Transform _centerEyeAnchor;
        [SerializeField] private CapsuleCollider _thisCollider;
        private GameObject _colliderGO;
        private const int MAXHEALTH = 100;

        #endregion

        #region Properties

        public int MaxHealth => MAXHEALTH;
        public Vector3 ColliderPos => _colliderGO.transform.position;
        public int Health
        {
            get => _health;
            set
            {
                if (value > MAXHEALTH)
                    _health = MAXHEALTH;
                else
                    _health = value;
                OnHealthChange.Invoke(_health);
            }
        }

        #endregion

        public UnityEvent<int> OnHealthChange;

        private void Awake()
        {
            if(_thisCollider == null)return;
            _colliderGO = _thisCollider.gameObject;
            OnHealthChange.AddListener(CheckForDeath);
        }

        private void Update()
        {
            if(_centerEyeAnchor == null) return;
            _thisCollider.height = _centerEyeAnchor.position.y;
            _colliderGO.transform.position = new Vector3(_centerEyeAnchor.position.x, _thisCollider.height * 0.5f,
                _centerEyeAnchor.position.z);
        }

        private void CheckForDeath(int newHealthValue)
        {
            if (newHealthValue > 0)return;

            GameManager.Instance.CurrState = EGameStates.GameOver;
        }

        public void HealPlayer(int amount)
        {
            Health += amount;
        }
    }
}
