using System.Collections.Generic;
using Player;
using UnityEngine;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _availableWeaponPrefabs;
        [SerializeField] private Transform _weaponSpawn;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        public void SpawnBoughtWeapon(int vendingNumber)
        {
            if (vendingNumber >= _availableWeaponPrefabs.Count || vendingNumber < 0)return;
            
            Instantiate(_availableWeaponPrefabs[vendingNumber], _weaponSpawn.position, Quaternion.identity);
        }
    }
}
