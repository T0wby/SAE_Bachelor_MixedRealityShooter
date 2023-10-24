using System;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Enemies
{
    public abstract class AEnemy : MonoBehaviour, IDamage, IPoolable<AEnemy>
    {
        #region Variables

        [SerializeField] protected EnemySettings _settings;
        protected ObjectPool<AEnemy> _pool;
        protected int _healthPotionAmount = 0;
        protected int _currHealth = 0;
        protected WaveManager _waveManager;

        #endregion

        #region Properties

        public EnemySettings Settings => _settings;
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
        public WaveManager WaveManager
        {
            get => _waveManager;
            set => _waveManager = value;
        }

        #endregion
        
        #region Events

        public UnityEvent<int> OnHealthChange;

        #endregion

        #region Virtual Methods

        public virtual void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        #region Pooling Methods

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
