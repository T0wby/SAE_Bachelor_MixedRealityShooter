using System;
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
