using System;
using System.Collections;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Enemies.TeleportRangeEnemy
{
    public class EnemyTP : AEnemy
    {
        #region Variables

        [SerializeField] private EnemyTargetDetection _ownTargetDetection;
        [SerializeField] private Transform _weaponSlot;
        
        private Transform _destination;
        private AWeapon _activeWeapon;
        private bool _canMove = true;
        private Vector3 _playerPos;
        private PlayerInventory _playerInventory;

        #endregion

        #region Properties

        public Transform Destination{ get => _destination; set => _destination = value; }
        public bool CanMove => _canMove;

        #endregion

        #region Unity Loop

        private void Awake()
        {
            SetDefaultStats();
            _ignoreLayers = LayerMask.NameToLayer("Enemy");
            _ignoreLayers = ~_ignoreLayers;
            OnHealthChange.AddListener(OnDeath);
            SpawnWeapon();
        }

        private void OnEnable()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Update()
        {
            if (_ownTargetDetection == null || _ownTargetDetection.Player == null) return;
            _playerPos = _ownTargetDetection.Player.transform.position;
            transform.rotation = Quaternion.LookRotation(_playerPos - transform.position, Vector3.up);
            RotateWeaponToTarget();
        }

        #endregion

        #region Methods

        private void SetDefaultStats()
        {
            CurrHealth = _settings.Health;
            _healthPotionAmount = _settings.HealthPotionAmount;
        }

        private void SpawnWeapon()
        {
            var wpnobj = Instantiate(_settings.Weapon, _weaponSlot.position, Quaternion.identity, _weaponSlot);
            _activeWeapon = wpnobj.GetComponent<AWeapon>();
        }

        private void RotateWeaponToTarget()
        {
            if(_activeWeapon == null)return;
            //_playerPos.y *= 0.5f;
            _activeWeapon.transform.rotation = Quaternion.LookRotation(_playerPos - _activeWeapon.transform.position, Vector3.up);
        }

        public void StartTeleport()
        {
            StartCoroutine(Teleport());
        }
        
        private IEnumerator Teleport()
        {
            if (_canMove)
            {
                _destination = _ownTargetDetection.GetSpawnPointTransform(true);
                if (_destination != null)
                {
                    _canMove = false;
                    if (_destination.position.y != 0.5f)
                        gameObject.transform.position = _destination.position + (transform.localScale * 0.5f);
                    else
                        gameObject.transform.position = _destination.position;
                    _ownTargetDetection.transform.localPosition = Vector3.zero; // Resetting pos since it wanders off???
                    yield return new WaitForSeconds(_settings.MoveTimer);
                    _canMove = true; 
                }
                
            }

            yield return null;
        }

        public override void Attack()
        {
            StartCoroutine(StartAttack());
        }

        private IEnumerator StartAttack()
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

        

        public override void TakeDamage(int damage)
        {
            CurrHealth -= damage;
        }

        public bool CheckForPlayerInSight()
        {
            if (_ownTargetDetection.Player == null) return false;
            
            float angle = Vector3.Angle(transform.position, _ownTargetDetection.Player.transform.position);

            if (angle <= _settings.FOV)
            {
                Vector3 dir = _ownTargetDetection.Player.transform.position - _weaponSlot.transform.position;
                dir.y *= 0.5f;
                if (Physics.Raycast(_weaponSlot.transform.position, dir, out var hit, Mathf.Infinity, _ignoreLayers))
                {
                    Debug.DrawRay(_weaponSlot.transform.position, dir * 2, Color.red, 2.0f);
                    return hit.transform.CompareTag("Player");
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void OnDeath(int health)
        {
            if (health > 0)return;

            if (_playerInventory != null)
                _playerInventory.Money += _settings.MoneyValue;
            
            _waveManager.RemoveDeadEnemy(this);
            ReturnEnemy();
        }

        #region Pool Methods

        public override void Initialize(ObjectPool<AEnemy> pool)
        {
            _pool = pool;
        }

        public override void Reset()
        {
            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            SetDefaultStats();
            gameObject.SetActive(false);
        }

#endregion

        #endregion
    }
}
