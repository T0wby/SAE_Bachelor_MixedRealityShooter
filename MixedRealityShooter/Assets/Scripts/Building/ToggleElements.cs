using UnityEngine;

namespace Building
{
    public class ToggleElements : MonoBehaviour
    {
        [SerializeField] private GameObject _elementToToggle;

        public void ToggleElement()
        {
            _elementToToggle.SetActive(!_elementToToggle.activeSelf);
        }
    }
}
