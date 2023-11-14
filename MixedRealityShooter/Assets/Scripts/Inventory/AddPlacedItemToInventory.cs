using System;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Inventory
{
    public class AddPlacedItemToInventory : MonoBehaviour
    {
        [Header("Reference")] [SerializeField] private Transform _rangeSpawn;
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
            if (_playerInventory == null) return;

            if (_playerInventory.ActiveRangeWeaponPrefab != null)
            {
                _playerInventory.ActiveRangeWeaponPrefab.transform.rotation = _rangeSpawn.rotation;
                _playerInventory.ActiveRangeWeaponPrefab.transform.position = _rangeSpawn.position;
                var rbRange = _playerInventory.ActiveRangeWeaponPrefab.GetComponent<Rigidbody>();
                if (rbRange != null)
                {
                    rbRange.useGravity = false;
                }

                _playerInventory.ActiveRangeWeaponPrefab.SetActive(true);
            }

            if (_playerInventory.ActiveMeleeWeaponPrefab == null) return;
            _playerInventory.ActiveMeleeWeaponPrefab.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            _playerInventory.ActiveMeleeWeaponPrefab.transform.position = _meleeSpawn.position;
            var rbMelee = _playerInventory.ActiveMeleeWeaponPrefab.GetComponent<Rigidbody>();
            if (rbMelee != null)
            {
                rbMelee.useGravity = false;
            }

            _playerInventory.ActiveMeleeWeaponPrefab.SetActive(true);
        }
    }
}