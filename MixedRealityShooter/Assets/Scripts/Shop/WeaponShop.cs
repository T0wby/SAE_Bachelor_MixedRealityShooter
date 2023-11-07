using System;
using System.Collections.Generic;
using Inventory;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _availableWeaponPrefabs;
        [SerializeField] private List<AWeapon> _availableWeapons;
        [SerializeField] private Transform _weaponSpawn;
        [SerializeField] private InventoryUpdater _inventoryUpdater;
        private AddPlacedItemToInventory _propAdder;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _inventoryUpdater = FindObjectOfType<InventoryUpdater>();
            _propAdder = FindObjectOfType<AddPlacedItemToInventory>();
        }

        public void SpawnBoughtWeapon(int vendingNumber)
        {
            if (vendingNumber >= _availableWeaponPrefabs.Count || vendingNumber < 0)return;
            
            Instantiate(_availableWeaponPrefabs[vendingNumber], _weaponSpawn.position, Quaternion.identity, _weaponSpawn);
        }
        
        public void AddBoughtWeaponToInven(int vendingNumber)
        {
            if (vendingNumber >= _availableWeaponPrefabs.Count || vendingNumber < 0)return;
            
            AWeapon wpn = _availableWeaponPrefabs[vendingNumber].GetComponent<AWeapon>();
            if (wpn == null)return;
            switch (wpn.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                    _playerInventory.AddRangeWeapon(_availableWeapons[vendingNumber]);
                    //_propAdder.SpawnRangeProp();
                    break;
                case EWeaponType.Pistol:
                    _playerInventory.AddRangeWeapon(_availableWeapons[vendingNumber]);
                    //_propAdder.SpawnRangeProp();
                    break;
                case EWeaponType.Dagger:
                    _playerInventory.AddMeleeWeapon(_availableWeapons[vendingNumber]);
                    //_propAdder.SpawnMeleeProp();
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _propAdder.SetActiveWeaponsToRack();
            
            _inventoryUpdater.UpdateFields();
        }
    }
}
