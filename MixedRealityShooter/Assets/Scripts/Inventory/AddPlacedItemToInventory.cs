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
        [SerializeField] private Transform _rangeSpawn;
        [SerializeField] private Transform _meleeSpawn;
        private PlayerInventory _playerInventory;
        private AWeapon _weapon;
        private GameObject _spawnedRangeProp;
        private GameObject _spawnedMeleeProp;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
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
                _playerInventory.ActiveRangeWeaponPrefab.SetActive(true);
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
                _playerInventory.ActiveMeleeWeaponPrefab.SetActive(true);
            }
        }
    }
}
