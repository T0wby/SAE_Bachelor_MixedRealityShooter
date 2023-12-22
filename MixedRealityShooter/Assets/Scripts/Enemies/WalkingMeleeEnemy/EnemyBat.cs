using System.Collections;
using PlacedObjects;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utility;
using Weapons;
using Random = UnityEngine.Random;

namespace Enemies.WalkingMeleeEnemy
{
    public class EnemyBat : AEnemy
    {
        [Header("Navigation")]
        [SerializeField] private NavMeshAgent _agent;
        [Header("Weapon")]
        [SerializeField] private Transform _weaponSlot;
        [SerializeField] private Transform _rotationSlot;
        private PlayerInventory _playerInventory;
        private MeleeWeapon _activeWeapon;

        #region Unity Loop

        private void Awake()
        {
            SetDefaultStats();
            _ignoreLayers = LayerMask.NameToLayer("Enemy");
            _ignoreLayers = ~_ignoreLayers;
            OnHealthChange.AddListener(OnDeath);
            SpawnWeapon();
            _player = FindObjectOfType<PlayerDamageHandler>();
        }
        
        private void OnEnable()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            GetSpawnPoint();
        }

        private void Update()
        {
            _activeWeapon.transform.localPosition = Vector3.zero;
        }

        #endregion
        
        private void SpawnWeapon()
        {
            var wpnobj = Instantiate(_settings.Weapon, _weaponSlot.position, Quaternion.identity, _weaponSlot);
            wpnobj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.90f);
            wpnobj.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);

            _activeWeapon = wpnobj.GetComponent<MeleeWeapon>();
            _activeWeapon.RotationTransform = _rotationSlot;
        }

        private void GetSpawnPoint()
        {
            var walls = FindObjectsByType<PlacedWall>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (walls.Length <= 0) return;

            var wall = walls[Random.Range(0, walls.Length)]; 
            
            transform.position = wall.Spawn.position;
            wall.gameObject.SetActive(true);
        }
        
        private void SetDefaultStats()
        {
            CurrHealth = _settings.Health;
            _healthPotionAmount = _settings.HealthPotionAmount;
        }
        
        private void OnDeath(int health)
        {
            if (health > 0)return;

            if (_playerInventory != null)
                _playerInventory.Money += _settings.MoneyValue;
            
            _waveManager.RemoveDeadEnemy(this);
            ReturnEnemy();
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
        
        #region Pool Methods

        public override void Initialize(ObjectPool<AEnemy> pool)
        {
            _pool = pool;
        }

        public override void Reset()
        {
            if (_player == null)
                _player = FindObjectOfType<PlayerDamageHandler>();
            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            SetDefaultStats();
            gameObject.SetActive(false);
        }

        #endregion
    }
}
