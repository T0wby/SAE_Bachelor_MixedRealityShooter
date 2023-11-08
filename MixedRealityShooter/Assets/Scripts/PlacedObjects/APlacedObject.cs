using UnityEngine;

namespace PlacedObjects
{
    public abstract class APlacedObject : MonoBehaviour
    {
        [Header("Color Properties")]
        [Tooltip("Only used on none Items")]
        [SerializeField] protected Color _normalColor;
        [Tooltip("Only used on none Items")]
        [SerializeField] protected Color _selectedColor;
        [Tooltip("Only used on none Items")]
        [SerializeField] protected Color _gameColor;
        protected Material _ownMat;

        private void Awake()
        {
            _ownMat = GetComponent<MeshRenderer>().material;
            DontDestroyOnLoad(transform.parent.gameObject);
            SetNormalColor();
        }

        public void SetSelectedColor()
        {
            _ownMat.SetColor("_NormalColor", _selectedColor);
        }
        public void SetNormalColor()
        {
            _ownMat.SetColor("_NormalColor", _normalColor);
        }
        public void SetGameColor()
        {
            _ownMat.SetColor("_NormalColor", _gameColor);
        }
    }
}
