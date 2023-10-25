using System;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Items
{
    public class AddPlacedItemToInventory : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private AWeapon _weapon;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_playerInventory == null)return;

            _weapon = other.GetComponent<AWeapon>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (_weapon == null || _weapon.IsGrabbed)return;

            switch (_weapon.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                    _playerInventory.AddRangeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Pistol:
                    _playerInventory.AddRangeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Dagger:
                    _playerInventory.AddMeleeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
