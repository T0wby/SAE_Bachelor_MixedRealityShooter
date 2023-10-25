using System.Collections.Generic;
using Player;
using UnityEngine;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _availableWeaponPrefabs;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }
        
        
    }
}
