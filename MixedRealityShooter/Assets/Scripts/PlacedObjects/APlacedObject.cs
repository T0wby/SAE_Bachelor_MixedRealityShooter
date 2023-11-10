using UnityEngine;

namespace PlacedObjects
{
    public abstract class APlacedObject : MonoBehaviour
    {
        [Header("Color Properties")]
        [SerializeField] protected Color _normalColor;
        [SerializeField] protected Color _selectedColor;
        [SerializeField] protected Color _gameColor;
        protected Material _ownMat;

        private void Awake()
        {
            _ownMat = GetComponent<MeshRenderer>().material;
            DontDestroyOnLoad(transform.parent != null ? transform.parent.gameObject : gameObject);
            SetNormalColor();
        }

        public virtual void SetSelectedColor()
        {
            _ownMat.SetColor("_NormalColor", _selectedColor);
        }
        public virtual void SetNormalColor()
        {
            _ownMat.SetColor("_NormalColor", _normalColor);
        }
        public virtual void SetGameColor()
        {
            _ownMat.SetColor("_NormalColor", _gameColor);
        }
    }
}
