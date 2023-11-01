using System;
using Player;
using UnityEngine;
using Utility;
using Weapons;

namespace Inventory
{
    public class AddPlacedItemToInventory : MonoBehaviour
    {
        [Header("Reference")] 
        [SerializeField] private InventoryUpdater _inventoryUpdater;
        [SerializeField] private Transform _rangeSpawn;
        [SerializeField] private Transform _meleeSpawn;
        private PlayerInventory _playerInventory;
        private AWeapon _weapon;
        private GameObject _spawnedRangeProp;
        private GameObject _spawnedMeleeProp;

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _inventoryUpdater = GetComponent<InventoryUpdater>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_playerInventory == null)return;

            _weapon = other.GetComponent<AWeapon>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (_playerInventory == null || _weapon == null )return;

            if (_weapon.gameObject == other.gameObject)
            {
                _weapon = null;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_weapon == null || _weapon.IsGrabbed)return;

            switch (_weapon.DefaultSettings.WeaponType)
            {
                case EWeaponType.AssaultRifle:
                    _playerInventory.AddRangeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    SpawnProp(_rangeSpawn, ref _spawnedRangeProp, _weapon.DefaultSettings.WeaponProp);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Pistol:
                    _playerInventory.AddRangeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    SpawnProp(_rangeSpawn, ref _spawnedRangeProp, _weapon.DefaultSettings.WeaponProp);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Dagger:
                    _playerInventory.AddMeleeWeapon(_weapon.DefaultSettings.WeaponPrefab);
                    SpawnProp(_meleeSpawn, ref _spawnedMeleeProp, _weapon.DefaultSettings.WeaponProp);
                    _weapon.gameObject.SetActive(false);
                    break;
                case EWeaponType.Grenade:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void SpawnProp(Transform spawnPoint, ref GameObject spawnRef, GameObject propToSpawn)
        {
            if (spawnRef != null)
                Destroy(spawnRef);

            spawnRef = Instantiate(propToSpawn, spawnPoint.position, spawnPoint.rotation);
            _inventoryUpdater.UpdateFields();
        }
    }
}
