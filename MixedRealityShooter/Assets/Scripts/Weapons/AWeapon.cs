using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class AWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponSettings settings;
    }
}
