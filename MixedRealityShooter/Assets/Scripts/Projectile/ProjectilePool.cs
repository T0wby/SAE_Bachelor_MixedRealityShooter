using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Projectile
{
    public class ProjectilePool : Singleton<ProjectilePool>
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private int _poolSize = 50;

        private ObjectPool<RangeWeaponProjectile> _arPool;
        private List<RangeWeaponProjectile> _weapons;

        public ObjectPool<RangeWeaponProjectile> ArPool => _arPool;
        
        private new void Awake()
        {
            base.Awake();
            _arPool = new ObjectPool<RangeWeaponProjectile>(_projectilePrefab, _poolSize, transform);
        }
    }
}
