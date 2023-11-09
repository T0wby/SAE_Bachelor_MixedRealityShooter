using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "NewItemSettings", menuName = "Items/Settings", order = 0)]
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _itemImage;

        public string ItemName => _itemName;
        public Sprite ItemImage => _itemImage;
    }
}