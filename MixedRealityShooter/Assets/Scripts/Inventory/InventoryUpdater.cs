using System;
using System.Globalization;
using Player;
using Shop;
using TMPro;
using UnityEngine;
using Utility;
using Weapons;

namespace Inventory
{
    public class InventoryUpdater : MonoBehaviour
    {
        [Header("UpdateFields")] 
        [SerializeField] private GameObject _rangeUpdateFieldUI;
        [SerializeField] private GameObject _rangeUpdateFieldBTN;
        [SerializeField] private GameObject _meleeUpdateFieldUI;
        [SerializeField] private GameObject _meleeUpdateFieldBTN;
        
        [Header("UI_TextFields")]
        [SerializeField] private TMP_Text _dmgLevelRange;
        [SerializeField] private TMP_Text _dmgCurrentRange;
        [SerializeField] private TMP_Text _dmgCostRange;
        [SerializeField] private TMP_Text _bpsLevelRange;
        [SerializeField] private TMP_Text _bpsCurrentRange;
        [SerializeField] private TMP_Text _bpsCostRange;
        [SerializeField] private TMP_Text _dmgLevelMelee;
        [SerializeField] private TMP_Text _dmgCurrentMelee;
        [SerializeField] private TMP_Text _dmgCostMelee;
        
        private PlayerInventory _playerInventory;
        private WeaponShop _weaponShop;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _weaponShop = FindObjectOfType<WeaponShop>();
            UpdateFields();
        }

        private void Start()
        {
            if(_weaponShop != null)
                _weaponShop.onBuyingWeapon.AddListener(UpdateFields);
        }


        #region Upgrade/Downgrade Methods

        public void UpgradeRangeDamage()
        {
            if (_playerInventory == null || _playerInventory.ActiveRangeWeapon == null)return;

            UpgradeDamage(_playerInventory.ActiveRangeWeapon);
        }
        public void DowngradeRangeDamage()
        {
            if (_playerInventory == null || _playerInventory.ActiveRangeWeapon == null)return;
            
            DowngradeDamage(_playerInventory.ActiveRangeWeapon);
        }
        
        public void UpgradeRangeBps()
        {
            if (_playerInventory == null || _playerInventory.ActiveRangeWeapon == null)return;
            
            UpgradeBps(_playerInventory.ActiveRangeWeapon);
        }
        
        public void DowngradeRangeBps()
        {
            if (_playerInventory == null || _playerInventory.ActiveRangeWeapon == null)return;
            
            DowngradeBps(_playerInventory.ActiveRangeWeapon);
        }
        
        public void UpgradeMeleeDamage()
        {
            if (_playerInventory == null || _playerInventory.ActiveMeleeWeapon == null)return;
            
            UpgradeDamage(_playerInventory.ActiveMeleeWeapon);
        }
        
        public void DowngradeMeleeDamage()
        {
            if (_playerInventory == null || _playerInventory.ActiveMeleeWeapon == null)return;
            
            DowngradeDamage(_playerInventory.ActiveMeleeWeapon);
        }
        
        private void UpgradeDamage(AWeapon weapon)
        {
            if (weapon.CheckForMaxDmgLevel()) return;
            if (!PayDamageCost(weapon)) return;
            weapon.UpgradeDamage();
            SetCorrectStatLevelText();
            UpdateCost();
        }
        private void DowngradeDamage(AWeapon weapon)
        {
            weapon.DowngradeDamage();
            SetCorrectStatLevelText();
            UpdateCost();
        }
        
        private void UpgradeBps(AWeapon weapon)
        {
            if (weapon.CheckForMaxBpsLevel()) return;
            if (!PayBpsCost(weapon))return;
            weapon.UpgradeFireRate();
            SetCorrectStatLevelText();
            UpdateCost();
        }
        private void DowngradeBps(AWeapon weapon)
        {
            weapon.DowngradeFireRate();
            SetCorrectStatLevelText();
            UpdateCost();
        }

        private bool PayDamageCost(AWeapon weapon)
        {
            switch (weapon.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                case EWeaponType.Pistol:
                case EWeaponType.Revolver:
                    if (_playerInventory.ActiveRangeWeapon.DamageCost > _playerInventory.Money)return false;
                    _playerInventory.Money -= _playerInventory.ActiveRangeWeapon.DamageCost;
                    break;
                case EWeaponType.BatSaw:
                    if (_playerInventory.ActiveMeleeWeapon.DamageCost > _playerInventory.Money)return false;
                    _playerInventory.Money -= _playerInventory.ActiveMeleeWeapon.DamageCost;
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
        private bool PayBpsCost(AWeapon weapon)
        {
            switch (weapon.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                case EWeaponType.Pistol:
                case EWeaponType.Revolver:
                    if (_playerInventory.ActiveRangeWeapon.BpsCost > _playerInventory.Money)return false;
                    _playerInventory.Money -= _playerInventory.ActiveRangeWeapon.BpsCost;
                    break;
                default:
                    break;;
            }
            return true;
        }

        #endregion

        private void UpdateFields()
        {
            SetCorrectStatLevelText();
            SetFieldsAccordingToInventory();
            UpdateCost();
        }
        
        private void SetCorrectStatLevelText()
        {
            if (_playerInventory == null) return;
            if (_playerInventory.ActiveRangeWeapon != null)
            {
                _dmgLevelRange.text = _playerInventory.ActiveRangeWeapon.DamageLevel.ToString();
                _dmgCurrentRange.text = _playerInventory.ActiveRangeWeapon.CurrDamage.ToString();
                _bpsLevelRange.text = _playerInventory.ActiveRangeWeapon.FireRateLevel.ToString();
                _bpsCurrentRange.text = _playerInventory.ActiveRangeWeapon.CurrBulletsPerSec.ToString(CultureInfo.CurrentCulture);
            }

            if (_playerInventory.ActiveMeleeWeapon == null) return;
            _dmgLevelMelee.text = _playerInventory.ActiveMeleeWeapon.DamageLevel.ToString();
            _dmgCurrentMelee.text = _playerInventory.ActiveMeleeWeapon.CurrDamage.ToString();
        }

        private void SetFieldsAccordingToInventory()
        {
            if (_playerInventory == null) return;
            bool range = _playerInventory.ActiveRangeWeapon != null;
            bool melee = _playerInventory.ActiveMeleeWeapon != null;
            
            _rangeUpdateFieldUI.SetActive(range);
            _rangeUpdateFieldBTN.SetActive(range);
            _meleeUpdateFieldUI.SetActive(melee);
            _meleeUpdateFieldBTN.SetActive(melee);
        }

        private void UpdateCost()
        {
            if (_playerInventory.ActiveRangeWeapon != null)
            {
                _dmgCostRange.text = _playerInventory.ActiveRangeWeapon.DamageCost.ToString();
                _bpsCostRange.text = _playerInventory.ActiveRangeWeapon.BpsCost.ToString();
            }

            if (_playerInventory.ActiveMeleeWeapon == null) return;
            _dmgCostMelee.text = _playerInventory.ActiveMeleeWeapon.DamageCost.ToString();
        }
    }
}
