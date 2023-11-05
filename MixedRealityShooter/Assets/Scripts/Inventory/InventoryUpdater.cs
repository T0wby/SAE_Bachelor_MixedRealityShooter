using System;
using Player;
using TMPro;
using UnityEngine;
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
        [SerializeField] private TMP_Text _dmgCostRange;
        [SerializeField] private TMP_Text _dmgLevelMelee;
        [SerializeField] private TMP_Text _dmgCostMelee;
        
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            UpdateFields();
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
            weapon.UpgradeDamage();
            SetCorrectWeaponLevelText();
        }
        private void DowngradeDamage(AWeapon weapon)
        {
            weapon.DowngradeDamage();
            SetCorrectWeaponLevelText();
        }

        #endregion

        public void UpdateFields()
        {
            SetCorrectWeaponLevelText();
            SetFieldsAccordingToInventory();
        }
        
        private void SetCorrectWeaponLevelText()
        {
            if (_playerInventory == null) return;
            if (_playerInventory.ActiveRangeWeapon != null)
                _dmgLevelRange.text = _playerInventory.ActiveRangeWeapon.WeaponLevel.ToString();
            if (_playerInventory.ActiveMeleeWeapon != null)
                _dmgLevelMelee.text = _playerInventory.ActiveMeleeWeapon.WeaponLevel.ToString();
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
    }
}
