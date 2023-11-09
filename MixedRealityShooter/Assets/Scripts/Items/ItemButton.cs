using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        private PlaceableVRItem _placeableVRItemRef;
        public Image ItemImage => _image;
        public TMP_Text Name => _name;
        public Button ButtonRef => _button;
        public PlaceableVRItem PlaceableVRItemRef => _placeableVRItemRef;

        public void InitButton(PlaceableVRItem itemRef)
        {
            if (itemRef == null)return;
            _placeableVRItemRef = itemRef;
            _name.text = itemRef.Settings.ItemName;
            _image.sprite = itemRef.Settings.ItemImage;
            transform.localRotation = Quaternion.identity;
            
            _button.onClick.AddListener(() =>
            {
                Debug.LogWarning($"ItemName: {itemRef.Settings.ItemName}");
            });
        }
    }
}
