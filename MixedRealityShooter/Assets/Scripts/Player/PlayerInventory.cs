using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Items;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private RangeWeapon _activeRangeWeapon;
        private MeleeWeapon _activeMeleeWeapon;

        [SerializeField] private List<PlaceableVRItem> _placeableVRItems;
        // throwable Items?

        public List<PlaceableVRItem> PlaceableVRItems => _placeableVRItems;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
