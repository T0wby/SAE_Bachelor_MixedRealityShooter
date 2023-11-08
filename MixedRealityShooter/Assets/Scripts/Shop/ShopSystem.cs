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

        public void UpgradeDamage(AWeapon weapon)
        {
            weapon.UpgradeDamage();
        }

        public void UpgradeFireRate(AWeapon weapon)
        {
            weapon.UpgradeFireRate();
        }

        public void AddWall()
        {
            _inventory.PlaceableVRItems.Add(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Wall));
        }
        public void AddBarrell()
        {
            _inventory.PlaceableVRItems.Add(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Barrell));
        }
        
        public void AddRandomItem()
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    AddBarrell();
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
