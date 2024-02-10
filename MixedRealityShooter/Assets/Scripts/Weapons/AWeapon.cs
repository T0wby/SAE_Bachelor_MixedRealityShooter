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
    [RequireComponent( typeof(Rigidbody))]
    public abstract class AWeapon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected WeaponSettings _defaultSettings;
        [Header("GrabEvent")]
        [SerializeField] protected GrabInteractable _grabInteractable;
        protected int _damage;
        protected float _projectileSpeed;
        protected float _bulletsPerSecond;
        protected int _prevDamageLevel = 0;
        protected int _damageLevel = 0;
        protected int _prevFireRateLevel = 0;
        protected int _fireRateLevel = 0;
        protected bool _isGrabbed = false;
        private Rigidbody _thisRB;
        private int _damageCost = 3;
        private int _bpsCost = 2;

        private const float UPGRADE_STRENGTH = 0.1f;
        private const int MAX_LEVEL = 10;
        
        public WeaponSettings DefaultSettings => _defaultSettings;
        public bool IsGrabbed => _isGrabbed;

        public int DamageLevel
        {
            get => _damageLevel;
            set
            {
                _damageLevel = value;
                CalcCost();
            }
        }
        
        public int FireRateLevel
        {
            get => _fireRateLevel;
            set
            {
                _fireRateLevel = value;
                CalcCost();
            }
        }
        
        public int CurrDamage => _damage;
        public float CurrBulletsPerSec => _bulletsPerSecond;
        public int DamageCost => _damageCost;
        public int BpsCost => _bpsCost;

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

        /// <summary>
        /// returns true when max stat is achieved
        /// </summary>
        /// <returns></returns>
        public void UpgradeDamage()
        {
            if (CheckForMaxDmgLevel()) return;
            
            int tmp = (int)(_damage * UPGRADE_STRENGTH);
            if (tmp == 0)
                tmp = 1;
            
            _damage += tmp;
            SetAllPrevLevel();
            DamageLevel++;
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
            SetAllPrevLevel();
            DamageLevel--;
        }
        
        /// <summary>
        /// returns true when max stat is achieved
        /// </summary>
        /// <returns></returns>
        public void UpgradeFireRate()
        {
            if (CheckForMaxBpsLevel()) return;

            _bulletsPerSecond += (_bulletsPerSecond * UPGRADE_STRENGTH);
            SetAllPrevLevel();
            FireRateLevel++;
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
            SetAllPrevLevel();
            FireRateLevel--;
        }

        private void CalcCost()
        {
            if (_damageLevel > _prevDamageLevel)
            {
                _damageCost += (int)(_damageLevel * 5.35f);
            }
            else if (_fireRateLevel > _prevFireRateLevel)
            {
                _bpsCost += (int)(_fireRateLevel * 5.2f);
            }
            else if (_damageLevel < _prevDamageLevel)
            {
                _damageCost -= (int)(_prevDamageLevel * 5.35f);
            }
            else if (_fireRateLevel < _prevFireRateLevel)
            {
                _bpsCost -= (int)(_prevFireRateLevel * 5.2f);
            }
        }

        private void SetAllPrevLevel()
        {
            _prevFireRateLevel = FireRateLevel;
            _prevDamageLevel = DamageLevel;
        }

        public bool CheckForMaxDmgLevel()
        {
            return _damageLevel >= MAX_LEVEL;
        }
        public bool CheckForMaxBpsLevel()
        {
            return _fireRateLevel >= MAX_LEVEL;
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
