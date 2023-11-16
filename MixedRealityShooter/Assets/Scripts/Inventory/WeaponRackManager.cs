using System;
using Player;
using Shop;
using UnityEngine;
using Utility;
using Weapons;

namespace Inventory
{
    public class WeaponRackManager : MonoBehaviour
    {
        [Header("Reference")] 
        [SerializeField] private Transform _rangeSpawn;
        [SerializeField] private Transform _meleeSpawn;
        private PlayerInventory _playerInventory;
        private WeaponShop _weaponShop;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _weaponShop = FindObjectOfType<WeaponShop>();
            
        }

        private void Start()
        {
            if(_weaponShop != null)
                _weaponShop.onBuyingWeapon.AddListener(SetActiveWeaponsToRack);
            SetActiveWeaponsToRack();
        }

        public void SetActiveWeaponsToRack()
        {
            if (_playerInventory == null) return;

            SetWeaponComponents(_playerInventory.ActiveRangeWeaponPrefab, _rangeSpawn);

            SetWeaponComponents(_playerInventory.ActiveMeleeWeaponPrefab, _meleeSpawn);
        }
        
        private void SetWeaponComponents(GameObject weapon, Transform spawn)
        {
            if (weapon == null)return;
            weapon.transform.rotation = spawn.rotation;
            weapon.transform.position = spawn.position;
            var rb = weapon.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
            }

            weapon.SetActive(true);
        }
    }
}