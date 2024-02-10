using System;
using Manager;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;

namespace Enemies
{
    public abstract class AEnemy : MonoBehaviour, IDamage, IPoolable<AEnemy>
    {
        #region Variables

        [SerializeField] protected EnemySettings _settings;
        protected ObjectPool<AEnemy> _pool;
        protected int _healthPotionAmount = 0;
        [SerializeField] protected int _currHealth = 0;
        [Header("Weapon")]
        [SerializeField] protected Transform _weaponSlot;
        protected WaveManager _waveManager;
        protected PlayerDamageHandler _player;
        protected int _ignoreLayers;
        protected bool _isAttacking = false;
        protected bool _isFleeing = false;

        #endregion

        #region Properties

        public EnemySettings Settings => _settings;
        public int HealthPotionAmount => _healthPotionAmount;
        public int IgnoreLayer => _ignoreLayers;
        public bool IsAttacking => _isAttacking;
        public Transform PlayerTransform => _player.transform;
        public Transform WeaponTransform => _weaponSlot;
        public int CurrHealth
        {
            get => _currHealth;
            set
            {
                if (value > _settings.Health)
                    _currHealth = _settings.Health;
                else if (value < 0)
                    _currHealth = 0;
                else
                    _currHealth = value;
                
                onHealthChange.Invoke(_currHealth);
            }
        }
        public WaveManager WaveManager
        {
            get => _waveManager;
            set => _waveManager = value;
        }
        
        public bool IsFleeing
        {
            get => _isFleeing;
            set => _isFleeing = value;
        }

        #endregion
        
        #region Events

        public UnityEvent<int> onHealthChange;

        #endregion

        #region Virtual Methods

        public virtual void Attack(){}
        
        public virtual void Heal()
        {
            if(_healthPotionAmount <= 0) return;
            CurrHealth += _settings.HealthPotionStrength;
            _healthPotionAmount--;
        }
        
        public virtual void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        #region Pooling Methods
        
        public void ReturnEnemy()
        {
            _pool.ReturnItem(this);
        }

        public virtual void Initialize(ObjectPool<AEnemy> pool)
        {
            throw new NotImplementedException();
        }

        public virtual void Reset()
        {
            throw new NotImplementedException();
        }

        public virtual void Deactivate()
        {
            throw new NotImplementedException();
        }

#endregion

        #endregion
    }
}
