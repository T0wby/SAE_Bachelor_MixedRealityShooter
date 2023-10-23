using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Enemies
{
    public abstract class AEnemy : MonoBehaviour, IDamage, IPoolable<AEnemy>
    {
        protected ObjectPool<AEnemy> _pool;
        
        #region Events

        public UnityEvent<int> OnHealthChange;

        #endregion
        
        public virtual void Initialize(ObjectPool<AEnemy> pool)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Reset()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
