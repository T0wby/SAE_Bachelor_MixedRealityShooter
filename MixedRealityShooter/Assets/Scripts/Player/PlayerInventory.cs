using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Items;

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

        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void AddRangeWeapon(GameObject weaponPrefab)
        {
            RangeWeapon tmp = weaponPrefab.GetComponent<RangeWeapon>();
            if (tmp == null)return;

            _activeRangeWeapon = tmp;
            _activeRangeWeaponPrefab = weaponPrefab;
        }
        public void AddMeleeWeapon(GameObject weaponPrefab)
        {
            MeleeWeapon tmp = weaponPrefab.GetComponent<MeleeWeapon>();
            if (tmp == null)return;

            _activeMeleeWeapon = tmp;
            _activeMeleeWeaponPrefab = weaponPrefab;
        }
    }
}
