using System;
using System.Collections.Generic;
using Items;
using Player;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] private GameObject _invenButtonPrefab;
        [SerializeField] private GameObject _placeableItemsParent;
        private PlayerInventory _playerInventory;
        private ItemShop _itemShop;
        private List<GameObject> _generatedItemButtons;
        private List<PlaceableVRItem> _generatedReferences;

        private void Awake()
        {
            _generatedItemButtons = new List<GameObject>();
            _generatedReferences = new List<PlaceableVRItem>();
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _itemShop = FindObjectOfType<ItemShop>();
            
            if (_playerInventory != null)
                _playerInventory.onPlaceableInventoryChange.AddListener(UpdateInventoryUI);
            
            if (_itemShop == null) return;
            _itemShop.onBuyingItem.AddListener(UpdateInventoryUI);
        }

        private void Start()
        {
            UpdateInventoryUI();
        }

        private void UpdateInventoryUI()
        {
            if (_playerInventory == null)return;

            foreach (var item in _playerInventory.PlaceableVRItems)
            {
                if (item == null || _generatedReferences.Contains(item)) continue;
                var tmp = Instantiate(_invenButtonPrefab, _placeableItemsParent.transform.position, Quaternion.identity,
                    _placeableItemsParent.transform);
                _generatedItemButtons.Add(tmp);
                _generatedReferences.Add(item);
                
                ItemButton itemButton = tmp.GetComponent<ItemButton>();
                if (itemButton == null)continue;
                itemButton.InitButton(item);
            }
        }
    }
}
