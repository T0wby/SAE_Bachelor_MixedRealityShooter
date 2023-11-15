using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.TeleportRangeEnemy;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Weapons
{
    [RequireComponent(typeof(GrabInteractable), typeof(Rigidbody))]
    public abstract class AWeapon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected WeaponSettings _defaultSettings;
        [Header("GrabEvent")]
        private GrabInteractable _grabInteractable;
        protected int _damage;
        protected float _projectileSpeed;
        protected float _bulletsPerSecond;
        protected int _damageLevel = 0;
        protected int _fireRateLevel = 0;
        protected bool _isGrabbed = false;
        private Rigidbody _thisRB;

        protected const float UPGRADE_STRENGTH = 0.1f;
        //protected const float BPSLIMIT = 5.0f;
        
        public WeaponSettings DefaultSettings => _defaultSettings;
        public bool IsGrabbed => _isGrabbed;
        public int DamageLevel => _damageLevel;
        public int FireRateLevel => _fireRateLevel;
        public int CurrDamage => _damage;
        public float CurrBulletsPerSec => _bulletsPerSecond;

        private void Awake()
        {
            InitWeapon();
            InitDefaultSettings();
        }

        private void OnEnable()
        {
            InitWeapon();
        }

        private void InitWeapon()
        {
            _thisRB = GetComponent<Rigidbody>();
            if (_grabInteractable != null) return;
            _grabInteractable = GetComponent<GrabInteractable>();
            if (_grabInteractable == null) return;
            _grabInteractable.WhenSelectingInteractorAdded.Action += OnGrabbed;
            _grabInteractable.WhenSelectingInteractorRemoved.Action += OnReleased;
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
            if (_damageLevel >= 10)return;
            
            int tmp = (int)(_damage * UPGRADE_STRENGTH);
            if (tmp == 0)
                tmp = 1;
            
            _damage += tmp;
            _damageLevel++;
        }
        
        /// <summary>
        /// Downgrade via rule of three
        /// </summary>
        public void DowngradeDamage()
        {
            if (_damageLevel == 0)return;

            float perc = UPGRADE_STRENGTH * 100 + 100;
            
            float tmp = ((_damage /perc) * 100);
            _damage = (int)tmp;
            _damageLevel--;
        }
        
        public void UpgradeFireRate()
        {
            if (_fireRateLevel >= 10) return;

            _bulletsPerSecond += (_bulletsPerSecond * UPGRADE_STRENGTH);
            _fireRateLevel++;
        }
        
        /// <summary>
        /// Downgrade via rule of three
        /// </summary>
        public void DowngradeFireRate()
        {
            if (_fireRateLevel == 0)return;

            float perc = UPGRADE_STRENGTH * 100 + 100;
            
            float tmp = ((_bulletsPerSecond / perc) * 100);
            _bulletsPerSecond = tmp;
            _fireRateLevel--;
        }

        protected virtual void OnGrabbed(GrabInteractor interactor)
        {
            _isGrabbed = true;
        }

        protected virtual void OnReleased(GrabInteractor interactor)
        {
            _isGrabbed = false;
            if (_thisRB != null)
            {
                _thisRB.useGravity = true;
            }
        }

    }
}
