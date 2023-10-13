using UnityEngine;

namespace PlacedObjects
{
    public abstract class APlacedObject : MonoBehaviour
    {
        [SerializeField] protected Color _normalColor;
        [SerializeField] protected Color _selectedColor;
        protected Material _ownMat;

        private void Awake()
        {
            _ownMat = GetComponent<MeshRenderer>().material;
        }

        public void SetSelectedColor()
        {
            _ownMat.SetColor("_Color", _selectedColor);
            _ownMat.SetFloat("_Alpha", _selectedColor.a);
        }
        public void SetNormalColor()
        {
            _ownMat.SetColor("_Color", _normalColor);
            _ownMat.SetFloat("_Alpha", _normalColor.a);
        }
    }
}
