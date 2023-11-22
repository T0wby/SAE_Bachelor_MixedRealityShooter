using Building;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Items
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        private PlaceableVRItem _placeableVRItemRef;
        private BuildModeGamePrep _buildMode;

        public void InitButton(PlaceableVRItem itemRef)
        {
            if (itemRef == null)return;
            _buildMode = FindObjectOfType<BuildModeGamePrep>();
            _placeableVRItemRef = itemRef;
            _name.text = itemRef.Settings.ItemName;
            _image.sprite = itemRef.Settings.ItemImage;
            transform.localRotation = Quaternion.identity;
            
            _button.onClick.AddListener(SetCurrItem);
        }

        private void SetCurrItem()
        {
            if (GameManager.Instance.CurrState != EGameStates.PreparePlayScene || _buildMode == null) return;
            _buildMode.SetCurrItem(_placeableVRItemRef);
        }
    }
}
