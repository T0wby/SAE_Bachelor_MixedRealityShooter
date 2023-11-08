using System;
using Items;
using UnityEngine;
using Utility;

namespace Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        #region Variables

        private PlaceableItemPool _wallPool;
        private PlaceableItemPool _barrellPool;

        #endregion

        #region Properties

        public PlaceableItemPool WallPool => _wallPool;
        public PlaceableItemPool BarrellPool => _barrellPool;

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
                        _wallPool = pool;
                        break;
                    case EPlaceableItemType.Barrell:
                        _barrellPool = pool;
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
                    return _wallPool.ItemPool.GetItem();
                case EPlaceableItemType.Barrell:
                    return _barrellPool.ItemPool.GetItem();
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
                    _wallPool.ItemPool.ReturnItem(item);
                    break;
                case EPlaceableItemType.Barrell:
                    _barrellPool.ItemPool.ReturnItem(item);
                    break;
                default:
                    break;;
            }
        }
    }
}
