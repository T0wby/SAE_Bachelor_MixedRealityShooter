using UnityEngine;

namespace PlacedObjects
{
    public abstract class APlacedObject : MonoBehaviour
    {
        [SerializeField] protected Color _normalColor;
        [SerializeField] protected Color _selectedColor;
        private Material _ownMat;

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
    }
}
