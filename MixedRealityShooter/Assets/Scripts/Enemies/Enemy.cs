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
        [SerializeField] private EnemyTargetDetection _ownTargetDetection;
        [SerializeField] private Transform _weaponSlot;
        private int _currHealth = 0;
        private int _healthPotionAmount = 0;
        private Transform _destination;
        private AWeapon _activeWeapon;
        private bool _isAttacking = false;
        private bool _canMove = true;
        private int _layermask;

        #endregion

        #region Properties

        public EnemySettings Settings => _settings;
        public Transform Destination
        {
            get => _destination;
            set => _destination = value;
        }
        public bool IsAttacking => _isAttacking;
        public bool CanMove => _canMove;
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
            _layermask = LayerMask.NameToLayer("Enemy");
            _layermask = ~_layermask;
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

        public void StartTeleport()
        {
            StartCoroutine(Teleport());
        }
        
        IEnumerator Teleport()
        {
            if (_canMove)
            {
                _canMove = false;
                transform.position = _destination.position;
                Quaternion.LookRotation(_ownTargetDetection.Player.transform.position, Vector3.up);
                yield return new WaitForSeconds(_settings.MoveTimer);
                _canMove = true;
            }

            yield return null;
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

        public bool CheckForPlayerInSight()
        {
            float angle = Vector3.Angle(transform.position, _ownTargetDetection.Player.transform.position);

            if (angle <= _settings.FOV)
            {
                if (Physics.Raycast(_weaponSlot.transform.position, _ownTargetDetection.Player.transform.position, out var hit, Mathf.Infinity, _layermask))
                {
                    return hit.transform.CompareTag("Player");
                }
                else
                    return false;
            }
            else
                return false;
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
