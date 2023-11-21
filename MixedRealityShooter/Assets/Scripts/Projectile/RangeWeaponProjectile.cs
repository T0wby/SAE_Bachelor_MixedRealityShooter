using System;
using UnityEngine;
using Utility;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class RangeWeaponProjectile : MonoBehaviour, IPoolable<RangeWeaponProjectile>
    {
        //TODO: Add settings for Projectiles
        private int _damage = 0;
        private bool _ignorePlayer = false;
        private ObjectPool<RangeWeaponProjectile> _pool;
        private Rigidbody _thisRb;
        private IDamage _objToDamage;

        public Rigidbody ThisRb => _thisRb;

        private void Awake()
        {
            _thisRb = GetComponent<Rigidbody>();
        }

        public void InitProjectileStats(int damage, bool ignorePlayer)
        {
            _damage = damage;
            _ignorePlayer = ignorePlayer;
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
        

        private void OnTriggerEnter(Collider other)
        {
            _objToDamage = other.GetComponent<IDamage>();
            if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                _pool.ReturnItem(this);
            }
            else if (_ignorePlayer && other.CompareTag("Player"))
            {
            }
            else if (_objToDamage != null)
            {
                _objToDamage.TakeDamage(_damage);
                _objToDamage = null;
                _pool.ReturnItem(this);
            }
        }
    }
}
