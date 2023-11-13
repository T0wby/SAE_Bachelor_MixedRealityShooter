using System;
using System.Collections.Generic;
using Items;
using Manager;
using PlacedObjects;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;

namespace Shop
{
    public class ItemShop : MonoBehaviour
    {
        private PlayerInventory _playerInventory;

        public UnityEvent onBuyingItem;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        public void AddItemToInventory(int itemType)
        {
            if (_playerInventory == null)return;

            var obj = ItemManager.Instance.ReceivePoolObject((EPlaceableItemType)itemType);
            if (obj.Settings.ItemCost > _playerInventory.Money)
            {
                obj.ReturnThisToPool();
            }
            else
            {
                _playerInventory.Money -= obj.Settings.ItemCost;
                _playerInventory.AddPlaceableVrItem(obj);
                onBuyingItem.Invoke();
            }
        }
    }
}
