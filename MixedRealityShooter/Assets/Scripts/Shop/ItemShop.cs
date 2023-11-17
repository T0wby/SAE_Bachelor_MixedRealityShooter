using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Manager;
using PlacedObjects;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utility;

namespace Shop
{
    public class ItemShop : MonoBehaviour
    {
        [Header("Items")]
        [SerializeField] private List<ItemSettings> _availableItemSettings;
        
        [Header("Text")] 
        [SerializeField] private TMP_Text _barrelCost;
        [SerializeField] private TMP_Text _wallCost;
        
        private PlayerInventory _playerInventory;

        public UnityEvent onBuyingItem;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            SetWeaponCostText();
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
        
        private void SetWeaponCostText()
        {
            foreach (var settings in _availableItemSettings.Where(settings => settings != null))
            {
                switch (settings.ItemType)
                {
                    case EPlaceableItemType.Wall:
                        _barrelCost.text = $"{settings.ItemCost}$";
                        break;
                    case EPlaceableItemType.Barrell:
                        _wallCost.text = $"{settings.ItemCost}$";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
