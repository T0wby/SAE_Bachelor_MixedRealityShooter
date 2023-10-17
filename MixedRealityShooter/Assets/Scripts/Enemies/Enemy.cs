using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Weapons;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamage, IPoolable<Enemy>
    {
        #region Variables

        [SerializeField] private EnemySettings _settings;
        private int _currHealth = 0;
        private int _healthPotionAmount = 0;
        private Transform _destination;
        private AWeapon _activeWeapon;

        #endregion

        #region Properties

        public EnemySettings Settings => _settings;
        public Transform Destination => _destination;
        public int CurrHealth
        {
            get => _currHealth;
            set
            {
                if (value > 100)
                    _currHealth = 100;
                else if (value < 0)
                    _currHealth = 0;
                else
                    _currHealth = value;
                
                OnHealthChange.Invoke(_currHealth);
            }
        }

        #endregion

        #region Events

        public UnityEvent<int> OnHealthChange;

        #endregion

        #region Unity Loop

        private void Awake()
        {
            SetDefaultStats();
        }

        #endregion

        #region Methods

        private void SetDefaultStats()
        {
            _currHealth = _settings.Health;
            _healthPotionAmount = _settings.HealthPotionAmount;
        }

        private void SpawnWeapon()
        {
            
        }
        
        public void SearchDestination()
        {
            
        }

        public void Heal()
        {
            if(_healthPotionAmount <= 0) return;
            CurrHealth += 20;
        }

        public void TakeDamage(int damage)
        {
            CurrHealth -= damage;
        }

        #region Pool Methods

        public void Initialize(ObjectPool<Enemy> pool)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

#endregion

        #endregion
    }
}
