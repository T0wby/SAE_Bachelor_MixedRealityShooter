using System;
using Items;
using UnityEngine;
using Utility;

namespace Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        #region Variables

        private PlaceableItemPool _spherePool;
        private PlaceableItemPool _cylinderPool;

        #endregion

        #region Properties

        public PlaceableItemPool SpherePool => _spherePool;
        public PlaceableItemPool CylinderPool => _cylinderPool;

        #endregion

        private void Start()
        {
            SortFoundPools();
        }
        
        /// <summary>
        /// Searches for pools of placeable Items and adds the reference according to their type.
        /// </summary>
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
        
        /// <summary>
        /// Returns an Item depending on the requested type
        /// </summary>
        /// <param name="type">Type to get</param>
        /// <returns>Returns the requested type or null</returns>
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

        public void ReturnPoolObject(PlaceableVRItem item)
        {
            switch (item.Type)
            {
                case EPlaceableItemType.NONE:
                    break;
                case EPlaceableItemType.Wall:
                    break;
                case EPlaceableItemType.Sphere:
                    _spherePool.ItemPool.ReturnItem(item);
                    break;
                case EPlaceableItemType.Cylinder:
                    _cylinderPool.ItemPool.ReturnItem(item);
                    break;
                default:
                    break;;
            }
        }
    }
}
