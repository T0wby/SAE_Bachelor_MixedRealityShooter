using System;
using UnityEngine;
using Utility;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class RangeWeaponProjectile : MonoBehaviour, IPoolable<RangeWeaponProjectile>
    {
        private ObjectPool<RangeWeaponProjectile> _pool;
        private Rigidbody _thisRb;

        private void Awake()
        {
            _thisRb = GetComponent<Rigidbody>();
        }

        public void Initialize(ObjectPool<RangeWeaponProjectile> pool)
        {
            _pool = pool;
        }
        
        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _thisRb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        
    }
}
