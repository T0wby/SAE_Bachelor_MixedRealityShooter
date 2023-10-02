using System;
using UnityEngine;
using Utility;
using Weapons;

namespace Shop
{
    public class ShopSystem : MonoBehaviour
    {
        
        
        public void UpgradeDamage(AWeapon weapon)
        {
            weapon.UpgradeDamage();
        }

        public void UpgradeFireRate(AWeapon weapon)
        {
            weapon.UpgradeFireRate();
        }
    }
}
