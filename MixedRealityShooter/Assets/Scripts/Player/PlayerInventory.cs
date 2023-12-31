using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Items;
using Manager;
using Utility;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Variables

        private RangeWeapon _activeRangeWeapon;
        private GameObject _activeRangeWeaponPrefab;
        private MeleeWeapon _activeMeleeWeapon;
        private GameObject _activeMeleeWeaponPrefab;

        [SerializeField] private List<PlaceableVRItem> _placeableVRItems;
        // throwable Items?

        #endregion

        #region Properties

        public List<PlaceableVRItem> PlaceableVRItems => _placeableVRItems;
        public GameObject ActiveRangeWeaponPrefab => _activeRangeWeaponPrefab;
        public GameObject ActiveMeleeWeaponPrefab => _activeMeleeWeaponPrefab;
        public RangeWeapon ActiveRangeWeapon => _activeRangeWeapon;
        public MeleeWeapon ActiveMeleeWeapon => _activeMeleeWeapon;

        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _placeableVRItems = new List<PlaceableVRItem>();
        }

        private void Start()
        {
            GameManager.Instance.OnGameStateChange.AddListener(ResetInventory);
            GameManager.Instance.OnGameStateChange.AddListener(SetInventoryInactive);
        }

        public void AddRangeWeapon(AWeapon weapon)
        {
            if (weapon == null|| weapon.GetType() != typeof(RangeWeapon))return;

            if (_activeRangeWeapon != null)
            {
                Destroy(_activeRangeWeaponPrefab);
            }
            
            _activeRangeWeaponPrefab = Instantiate(weapon.DefaultSettings.WeaponPrefab, transform.position, Quaternion.identity, transform);
            _activeRangeWeapon = _activeRangeWeaponPrefab.GetComponent<RangeWeapon>();
            _activeRangeWeaponPrefab.SetActive(false);
        }
        public void AddMeleeWeapon(AWeapon weapon)
        {
            if (weapon == null || weapon.GetType() != typeof(MeleeWeapon))return;

            if (_activeMeleeWeapon != null)
            {
                Destroy(_activeMeleeWeaponPrefab);
            }
            
            _activeMeleeWeaponPrefab = Instantiate(weapon.DefaultSettings.WeaponPrefab, transform.position, Quaternion.identity, transform);
            _activeMeleeWeapon = _activeMeleeWeaponPrefab.GetComponent<MeleeWeapon>();
            _activeMeleeWeaponPrefab.SetActive(false);
        }

        #region Event Methods

        private void ResetInventory(EGameStates state)
        {
            if(state != EGameStates.GameOver)return;

            if (_activeRangeWeapon != null)
            {
                Destroy(_activeRangeWeaponPrefab);
                _activeRangeWeapon = null;
            }
            if (_activeMeleeWeapon != null)
            {
                Destroy(_activeMeleeWeaponPrefab);
                _activeMeleeWeapon = null;
            }
            _placeableVRItems.Clear();
        }

        private void SetInventoryInactive(EGameStates state)
        {
            if (state != EGameStates.InHub)return;
            
            if (_activeRangeWeaponPrefab != null)
            {
                _activeRangeWeaponPrefab.SetActive(false);
            }
            if (_activeMeleeWeaponPrefab != null)
            {
                _activeMeleeWeaponPrefab.SetActive(false);
            }
        }

        #endregion
    }
}
