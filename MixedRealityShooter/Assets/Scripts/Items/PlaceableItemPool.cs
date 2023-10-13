using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Items
{
    public class PlaceableItemPool : Singleton<PlaceableItemPool>
    {
        [SerializeField] private GameObject _placeablePrefab;
        [SerializeField] private GameObject _placeablePrefabTwo;
        [SerializeField] private int _poolSize = 50;
        [SerializeField] private EPlaceableItemType _ePlaceable = EPlaceableItemType.Cylinder;

        private ObjectPool<PlaceableVRItem> _itemPool;

        public ObjectPool<PlaceableVRItem> ItemPool => _itemPool;
        public EPlaceableItemType EPlaceable => _ePlaceable;
        
        private new void Awake()
        {
            base.Awake();
            _itemPool = new ObjectPool<PlaceableVRItem>(_placeablePrefab, _poolSize, transform);
        }
    }
}
