using System;
using System.Collections;
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
        private bool _isAttacking = false;

        #endregion

        #region Properties

        public EnemySettings Settings => _settings;
        public Transform Destination => _destination;
        public bool IsAttacking => _isAttacking;
        public int HealthPotionAmount => _healthPotionAmount;
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

        public void StartAttack()
        {
            StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            if (!_isAttacking)
            {
                _isAttacking = true;
                _activeWeapon.Attack();
                yield return new WaitForSeconds(_settings.AttackTimer);
                _isAttacking = false;
            }

            yield return null;
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
