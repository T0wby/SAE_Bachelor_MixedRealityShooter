using System;
using Manager;
using Player;
using UnityEngine;
using Utility;
using Weapons;
using Random = UnityEngine.Random;

namespace Shop
{
    public class ShopSystem : MonoBehaviour
    {
        private PlayerInventory _inventory;
        
        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        private void AddWall()
        {
            _inventory.AddPlaceableVrItem(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Wall));
        }
        private void AddBarrel()
        {
            _inventory.AddPlaceableVrItem(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Barrell));
        }
        
        public void AddRandomItem()
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    AddBarrel();
                    break;
                case 1:
                    AddWall();
                    break;
                default:
                    break;
            }
        }
    }
}
