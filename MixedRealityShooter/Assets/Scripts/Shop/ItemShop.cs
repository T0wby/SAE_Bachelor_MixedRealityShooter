using System;
using System.Collections.Generic;
using Items;
using Manager;
using PlacedObjects;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Shop
{
    public class ItemShop : MonoBehaviour
    {
        private PlayerInventory _playerInventory;

        public UnityEvent OnBuyingItem;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        public void AddItemToInventory(int itemType)
        {
            if (_playerInventory == null)return;

            var obj = ItemManager.Instance.ReceivePoolObject((EPlaceableItemType)itemType);
            // TODO: Check Money
            if (obj.Settings.ItemCost > _playerInventory.Money)
            {
                obj.ReturnThisToPool();
            }
            else
            {
                _playerInventory.Money -= obj.Settings.ItemCost;
                _playerInventory.PlaceableVRItems.Add(ItemManager.Instance.ReceivePoolObject((EPlaceableItemType)itemType));
                OnBuyingItem.Invoke();
            }
        }
    }
}
