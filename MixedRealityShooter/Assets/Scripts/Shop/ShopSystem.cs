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

        public void AddSphere()
        {
            _inventory.PlaceableVRItems.Add(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Sphere));
        }
        public void AddCylinder()
        {
            _inventory.PlaceableVRItems.Add(ItemManager.Instance.ReceivePoolObject(EPlaceableItemType.Cylinder));
        }
        
        public void AddRandomItem()
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    AddCylinder();
                    break;
                case 1:
                    AddSphere();
                    break;
                default:
                    break;
            }
        }
    }
}
