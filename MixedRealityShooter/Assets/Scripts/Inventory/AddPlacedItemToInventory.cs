using System;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Inventory
{
    public class AddPlacedItemToInventory : MonoBehaviour
    {
        [Header("Reference")] 
        [SerializeField] private InventoryUpdater _inventoryUpdater;
        [SerializeField] private Transform _rangeSpawn;
        [SerializeField] private Transform _meleeSpawn;
        private PlayerInventory _playerInventory;
        private AWeapon _weapon;
        private GameObject _spawnedRangeProp;
        private GameObject _spawnedMeleeProp;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _inventoryUpdater = GetComponent<InventoryUpdater>();
            // SpawnRangeProp();
            // SpawnMeleeProp();
            SetActiveWeaponsToRack();
        }

        public void SetActiveWeaponsToRack()
        {
            if (_playerInventory == null)return;

            if (_playerInventory.ActiveRangeWeaponPrefab != null)
            {
                _playerInventory.ActiveRangeWeaponPrefab.transform.position = _rangeSpawn.position;
                _playerInventory.ActiveRangeWeaponPrefab.transform.rotation = _rangeSpawn.rotation;
                var rb = _playerInventory.ActiveRangeWeaponPrefab.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                }
            }
            
            if (_playerInventory.ActiveMeleeWeaponPrefab != null)
            {
                _playerInventory.ActiveMeleeWeaponPrefab.transform.position = _meleeSpawn.position;
                _playerInventory.ActiveMeleeWeaponPrefab.transform.rotation = _meleeSpawn.rotation;
                var rb = _playerInventory.ActiveMeleeWeaponPrefab.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                }
            }
        }

        public void SpawnRangeProp()
        {
            if (_playerInventory == null || _playerInventory.ActiveRangeWeapon == null)return;
            
            SpawnProp(_rangeSpawn, ref _spawnedRangeProp, _playerInventory.ActiveRangeWeapon.DefaultSettings.WeaponProp);
        }
        public void SpawnMeleeProp()
        {
            if (_playerInventory == null || _playerInventory.ActiveMeleeWeapon == null)return;
            SpawnProp(_meleeSpawn, ref _spawnedMeleeProp, _playerInventory.ActiveMeleeWeapon.DefaultSettings.WeaponProp);
        }

        private void SpawnProp(Transform spawnPoint, ref GameObject spawnRef, GameObject propToSpawn)
        {
            if (spawnRef != null)
                Destroy(spawnRef);

            spawnRef = Instantiate(propToSpawn, spawnPoint.position, spawnPoint.rotation);
            _inventoryUpdater.UpdateFields();
        }
    }
}
