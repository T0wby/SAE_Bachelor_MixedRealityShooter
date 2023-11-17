using UnityEngine;
using Utility;

namespace Items
{
    [CreateAssetMenu(fileName = "NewItemSettings", menuName = "Items/Settings", order = 0)]
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemImage;
        [SerializeField] private EPlaceableItemType _type;
        [SerializeField] private int _itemCost;

        public string ItemName => _itemName;
        public Sprite ItemImage => _itemImage;
        public EPlaceableItemType ItemType => _type;
        public int ItemCost => _itemCost;
    }
}