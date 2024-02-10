using UnityEngine;

namespace PlacedObjects
{
    public abstract class APlacedObject : MonoBehaviour
    {
        [Header("Color Properties")]
        [SerializeField] protected Color _normalColor;
        [SerializeField] protected Color _selectedColor;
        [SerializeField] protected Color _gameColor;
        [SerializeField] protected Renderer _ownRenderer;
        protected Material _ownMat;

        private void Awake()
        {
            if (_ownRenderer == null)
            {
                _ownRenderer = GetComponent<MeshRenderer>();
            }
            _ownMat = _ownRenderer.material;
            DontDestroyOnLoad(transform.parent != null ? transform.parent.gameObject : gameObject);
            SetNormalColor();
        }

        public virtual void SetSelectedColor()
        {
            _ownMat.SetColor("_WireframeColor", _selectedColor);
        }
        public virtual void SetNormalColor()
        {
            _ownMat.SetColor("_WireframeColor", _normalColor);
        }
        public virtual void SetGameColor()
        {
            _ownMat.SetColor("_WireframeColor", _gameColor);
            _ownMat.SetFloat("_WireframeWidth", 0.0f);
        }
    }
}
