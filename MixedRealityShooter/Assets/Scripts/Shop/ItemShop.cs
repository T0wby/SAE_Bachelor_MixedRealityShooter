using System;
using System.Collections.Generic;
using Items;
using PlacedObjects;
using Player;
using UnityEngine;

namespace Shop
{
    public class ItemShop : MonoBehaviour
    {
        [Header("Available Items To Buy")]
        [SerializeField] private List<PlaceableVRItem> _availableObjects;
        
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        public void AddItemToInventory(int shopNumber)
        {
            if (_playerInventory == null)return;
            // TODO: Check Money
            
            _playerInventory.PlaceableVRItems.Add(_availableObjects[shopNumber]);
        }
    }
}
