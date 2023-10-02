using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Weapons
{
    public abstract class AWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponSettings _defaultSettings;
        private int _damage;
        private float _projectileSpeed;
        private float _bulletsPerSecond;
        private int _weaponLevel = 0;

        private const float UPGRADE_STRENGTH = 0.1f;
        private const float BPSLIMIT_AR = 5.0f;
        
        public WeaponSettings DefaultSettings => _defaultSettings;

        private void Awake()
        {
            InitDefaultSettings();
        }

        private void InitDefaultSettings()
        {
            _damage = _defaultSettings.Damage;
            _projectileSpeed = _defaultSettings.ProjectileSpeed;
            _bulletsPerSecond = _defaultSettings.BulletsPerSecond;
        }

        public void UpgradeDamage()
        {
            if (_weaponLevel >= 10)return;
            
            int tmp = (int)(_damage * UPGRADE_STRENGTH);
            _damage += tmp;
            _weaponLevel++;
        }
        
        public void UpgradeFireRate()
        {
            if (_weaponLevel >= 10) return;

            switch (_defaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                    if (_bulletsPerSecond >= BPSLIMIT_AR)return;
                    _bulletsPerSecond += (_bulletsPerSecond * UPGRADE_STRENGTH); 
                    break;
                default:
                    return;
            }
            _weaponLevel++;
        }

    }
}
