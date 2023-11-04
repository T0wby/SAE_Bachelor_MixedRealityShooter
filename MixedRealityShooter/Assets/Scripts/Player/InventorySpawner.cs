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

            if (_playerInventory.ActiveRangeWeaponPrefab != null)
            {
                _playerInventory.ActiveRangeWeaponPrefab.GetComponent<Rigidbody>().useGravity = false;
                _playerInventory.ActiveRangeWeaponPrefab.SetActive(true);
                _playerInventory.ActiveRangeWeaponPrefab.transform.position = _rangeWeaponSpawn.position;
            }

            if (_playerInventory.ActiveMeleeWeaponPrefab != null)
            {
                _playerInventory.ActiveMeleeWeaponPrefab.SetActive(true);
                _playerInventory.ActiveMeleeWeaponPrefab.transform.position = _meleeWeaponSpawn.position;
            }
        }
    }
}
