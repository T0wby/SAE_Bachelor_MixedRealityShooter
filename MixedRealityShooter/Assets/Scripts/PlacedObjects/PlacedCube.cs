using System;
using UnityEngine;

namespace PlacedObjects
{
    public class PlacedCube : MonoBehaviour
    {
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;
        private Material _ownMat;

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
