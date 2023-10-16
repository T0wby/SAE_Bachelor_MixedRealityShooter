using PlacedObjects;
using UnityEngine;
using Utility;

namespace Items
{
    public class PlaceableVRItem : APlacedObject, IPoolable<PlaceableVRItem>, IDamage
    {
        private ObjectPool<PlaceableVRItem> _pool;
        [SerializeField] private EPlaceableItemType _type;
        
        public EPlaceableItemType Type => _type;
        
        public void Initialize(ObjectPool<PlaceableVRItem> pool)
        {
            _pool = pool;
        }

        public void Reset()
        {
            
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"{gameObject.name} took {damage} damage!");
        }
    }
}
