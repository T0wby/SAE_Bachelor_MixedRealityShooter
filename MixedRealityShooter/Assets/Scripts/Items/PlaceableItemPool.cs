using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Items
{
    public class PlaceableItemPool : MonoBehaviour
    {
        [SerializeField] private GameObject _placeablePrefab;
        [SerializeField] private int _poolSize = 50;
        [SerializeField] private EPlaceableItemType _ePlaceable = EPlaceableItemType.Barrell;

        private ObjectPool<PlaceableVRItem> _itemPool;

        public ObjectPool<PlaceableVRItem> ItemPool => _itemPool;
        public EPlaceableItemType EPlaceable => _ePlaceable;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _itemPool = new ObjectPool<PlaceableVRItem>(_placeablePrefab, _poolSize, transform);
        }
    }
}
