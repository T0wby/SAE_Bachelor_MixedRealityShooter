using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Projectile
{
    public class ProjectilePool : Singleton<ProjectilePool>
    {
        [SerializeField] private GameObject _arProjectilePrefab;
        [SerializeField] private int _poolSize = 50;

        private ObjectPool<RangeWeaponProjectile> _arPool;
        private List<RangeWeaponProjectile> _weapons;
        
        private new void Awake()
        {
            base.Awake();
            _arPool = new ObjectPool<RangeWeaponProjectile>(_arProjectilePrefab, _poolSize, transform);
        }
    }
}
