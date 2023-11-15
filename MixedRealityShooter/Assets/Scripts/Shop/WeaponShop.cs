using System;
using System.Collections.Generic;
using Inventory;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] private List<GameObject> _availableWeaponPrefabs;
        [SerializeField] private List<AWeapon> _availableWeapons;

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
            foreach (var obj in _availableWeaponPrefabs)
            {
                if (obj == null)continue;
                var wpn = obj.GetComponent<AWeapon>();
                if (wpn == null)continue;
                switch (wpn.DefaultSettings.WeaponType)
                {
                    case EWeaponType.AssaultRifle:
                        _arCost.text = $"{wpn.DefaultSettings.Value.ToString()}$";
                        break;
                    case EWeaponType.Pistol:
                        _pistolCost.text = $"{wpn.DefaultSettings.Value.ToString()}$";
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
            if (vendingNumber >= _availableWeaponPrefabs.Count || vendingNumber < 0)return;
            
            AWeapon wpn = _availableWeaponPrefabs[vendingNumber].GetComponent<AWeapon>();
            if (wpn == null)return;
            if (wpn.DefaultSettings.Value > _playerInventory.Money)return;
            
            switch (wpn.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                case EWeaponType.Revolver:
                case EWeaponType.Pistol:
                    _playerInventory.AddRangeWeapon(wpn);
                    break;
                case EWeaponType.Dagger:
                    _playerInventory.AddMeleeWeapon(wpn);
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _playerInventory.Money -= wpn.DefaultSettings.Value;
            
            onBuyingWeapon.Invoke();
        }
    }
}
