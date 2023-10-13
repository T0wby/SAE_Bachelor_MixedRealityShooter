using System;
using Items;
using UnityEngine;
using Utility;

namespace Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        private PlaceableItemPool _spherePool;
        private PlaceableItemPool _cylinderPool;

        public PlaceableItemPool SpherePool => _spherePool;
        public PlaceableItemPool CylinderPool => _cylinderPool;

        private void Start()
        {
            SortFoundPools();
        }

        private void SortFoundPools()
        {
            var pools = FindObjectsOfType<PlaceableItemPool>();
            foreach (var pool in pools)
            {
                switch (pool.EPlaceable)
                {
                    case EPlaceableItemType.NONE:
                        break;
                    case EPlaceableItemType.Wall:
                        break;
                    case EPlaceableItemType.Sphere:
                        _spherePool = pool;
                        break;
                    case EPlaceableItemType.Cylinder:
                        _cylinderPool = pool;
                        break;
                    default:
                        break;
                }
            }
        }

        public PlaceableVRItem ReceivePoolObject(EPlaceableItemType type)
        {
            switch (type)
            {
                case EPlaceableItemType.NONE:
                    break;
                case EPlaceableItemType.Wall:
                    break;
                case EPlaceableItemType.Sphere:
                    return _spherePool.ItemPool.GetItem();
                case EPlaceableItemType.Cylinder:
                    return _cylinderPool.ItemPool.GetItem();
                default:
                    return null;
            }
            return null;
        }
    }
}
