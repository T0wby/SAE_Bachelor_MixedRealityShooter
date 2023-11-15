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
                var rb = _playerInventory.ActiveRangeWeaponPrefab.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                _playerInventory.ActiveRangeWeaponPrefab.SetActive(true);
                _playerInventory.ActiveRangeWeaponPrefab.transform.rotation = _rangeWeaponSpawn.rotation;
                _playerInventory.ActiveRangeWeaponPrefab.transform.position = _rangeWeaponSpawn.position;
            }

            if (_playerInventory.ActiveMeleeWeaponPrefab == null) return;
            _playerInventory.ActiveMeleeWeaponPrefab.SetActive(true);
            _playerInventory.ActiveMeleeWeaponPrefab.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            _playerInventory.ActiveMeleeWeaponPrefab.transform.position = _meleeWeaponSpawn.position;
        }
    }
}
