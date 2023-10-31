using System;
using Manager;
using UnityEngine;
using Utility;

namespace Player
{
    public class InventorySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _rangeWeaponSpawn;
        [SerializeField] private Transform _meleeWeaponSpawn;
        private PlayerInventory _playerInventory;

        private void OnEnable()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            SpawnInventory(GameManager.Instance.CurrState);
        }

        private void SpawnInventory(EGameStates state)
        {
            if (_playerInventory == null || state != EGameStates.InGame)return;

            if(_playerInventory.ActiveRangeWeapon != null)
                Instantiate(_playerInventory.ActiveRangeWeapon.gameObject, _rangeWeaponSpawn.position, Quaternion.identity);
            if(_playerInventory.ActiveMeleeWeapon != null)
                Instantiate(_playerInventory.ActiveMeleeWeapon.gameObject, _meleeWeaponSpawn.position, Quaternion.identity);
        }
    }
}
