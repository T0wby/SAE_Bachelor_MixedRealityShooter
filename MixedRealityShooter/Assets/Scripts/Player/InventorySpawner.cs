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

        private void Awake()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            GameManager.Instance.OnGameStateChange.AddListener(SpawnInventory);
        }

        private void SpawnInventory(EGameStates state)
        {
            if (state != EGameStates.InGame)return; // Might need to do it on Enable instead

            Instantiate(_playerInventory.ActiveRangeWeaponPrefab, _rangeWeaponSpawn.position, Quaternion.identity);
            Instantiate(_playerInventory.ActiveMeleeWeaponPrefab, _meleeWeaponSpawn.position, Quaternion.identity);
        }
    }
}
