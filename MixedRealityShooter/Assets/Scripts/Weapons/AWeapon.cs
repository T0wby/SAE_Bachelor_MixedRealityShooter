using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.TeleportRangeEnemy;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Weapons
{
    public abstract class AWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSettings _defaultSettings;
        protected int _damage;
        protected float _projectileSpeed;
        protected float _bulletsPerSecond;
        protected int _weaponLevel = 0;
        protected bool _isGrabbed = false;
        private Rigidbody _thisRB;

        protected const float UPGRADE_STRENGTH = 0.1f;
        protected const float BPSLIMIT_AR = 5.0f;
        
        public WeaponSettings DefaultSettings => _defaultSettings;
        public bool IsGrabbed => _isGrabbed;
        public int WeaponLevel => _weaponLevel;

        private void Awake()
        {
            InitDefaultSettings();
            _thisRB = GetComponent<Rigidbody>();
        }

        private void InitDefaultSettings()
        {
            _damage = _defaultSettings.Damage;
            _projectileSpeed = _defaultSettings.ProjectileSpeed;
            _bulletsPerSecond = _defaultSettings.BulletsPerSecond;
        }

        public virtual void Attack(){ }

        public void UpgradeDamage()
        {
            if (_weaponLevel >= 10)return;
            
            int tmp = (int)(_damage * UPGRADE_STRENGTH);
            _damage += tmp;
            _weaponLevel++;
        }
        
        /// <summary>
        /// Downgrade via rule of three
        /// </summary>
        public void DowngradeDamage()
        {
            if (_weaponLevel == 0)return;

            int perc = (int)UPGRADE_STRENGTH * 100 + 100;
            
            int tmp = ((_damage /perc) * 100);
            _damage = tmp;
            _weaponLevel--;
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
        
        public void OnGrabbed()
        {
            _isGrabbed = true;
        }
        public void OnReleased()
        {
            _isGrabbed = false;
            if (_thisRB != null)
            {
                _thisRB.useGravity = true;
            }
        }

    }
}
