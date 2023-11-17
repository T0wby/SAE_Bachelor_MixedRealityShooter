using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] private List<WeaponSettings> _availableWeaponSettings;

        [Header("Text")] 
        [SerializeField] private TMP_Text _arCost;
        [SerializeField] private TMP_Text _pistolCost;
        
        private PlayerInventory _playerInventory;
        
        public UnityEvent onBuyingWeapon;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            SetWeaponCostText();
        }

        private void SetWeaponCostText()
        {
            foreach (var settings in _availableWeaponSettings.Where(obj => obj != null))
            {
                switch (settings.WeaponType)
                {
                    case EWeaponType.AssaultRifle:
                        _arCost.text = $"{settings.Value}$";
                        break;
                    case EWeaponType.Pistol:
                        _pistolCost.text = $"{settings.Value}$";
                        break;
                    case EWeaponType.Revolver:
                        break;
                    case EWeaponType.Dagger:
                        break;
                    case EWeaponType.Grenade:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public void AddBoughtWeaponToInven(int vendingNumber)
        {
            if (vendingNumber >= _availableWeaponSettings.Count || vendingNumber < 0)return;

            var settings = _availableWeaponSettings[vendingNumber];
            
            if (settings.Value > _playerInventory.Money)return;
            
            switch (settings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                case EWeaponType.Revolver:
                case EWeaponType.Pistol:
                    _playerInventory.AddRangeWeapon(settings.WeaponPrefab.GetComponent<AWeapon>());
                    break;
                case EWeaponType.Dagger:
                    _playerInventory.AddMeleeWeapon(settings.WeaponPrefab.GetComponent<AWeapon>());
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _playerInventory.Money -= settings.Value;
            
            onBuyingWeapon.Invoke();
        }
    }
}
