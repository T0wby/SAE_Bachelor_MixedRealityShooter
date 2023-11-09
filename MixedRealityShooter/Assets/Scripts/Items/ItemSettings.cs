using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "NewItemSettings", menuName = "Items/Settings", order = 0)]
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemImage;
        [SerializeField] private int _itemCost;

        public string ItemName => _itemName;
        public Sprite ItemImage => _itemImage;
        public int ItemCost => _itemCost;
    }
}