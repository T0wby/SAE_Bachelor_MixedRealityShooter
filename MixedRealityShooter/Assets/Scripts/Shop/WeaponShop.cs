using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Shop
{
    public class WeaponShop : MonoBehaviour
    {
        [SerializeField] private List<AWeapon> _availableWeapons;
    }
}
