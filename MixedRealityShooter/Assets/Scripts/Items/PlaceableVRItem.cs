using PlacedObjects;
using UnityEngine;
using Utility;

namespace Items
{
    public class PlaceableVRItem : APlacedObject, IPoolable<PlaceableVRItem>, IDamage
    {
        private ObjectPool<PlaceableVRItem> _pool;
        [SerializeField] private ItemSettings _settings;
        [SerializeField] private EPlaceableItemType _type;
        
        public ItemSettings Settings => _settings;
        public EPlaceableItemType Type => _type;

        public override void SetGameColor()
        {
            _ownMat.SetColor("_Color", _gameColor);
        }

        public override void SetNormalColor()
        {
            _ownMat.SetColor("_Color", _normalColor);
        }

        public override void SetSelectedColor()
        {
            _ownMat.SetColor("_Color", _selectedColor);
        }

        #region Interface Methods

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
            //Debug.Log($"{gameObject.name} took {damage} damage!");
        }

        #endregion
    }
}
