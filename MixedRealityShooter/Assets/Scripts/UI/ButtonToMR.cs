using Manager;
using Oculus.Interaction;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class ButtonToMR : MonoBehaviour
    {
        private PointableUnityEventWrapper _event;
        private PlayerInventory _inventory;

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
            _event = GetComponent<PointableUnityEventWrapper>();
            if (_event == null) return;
        
            _event.WhenSelect.AddListener(LoadMrScene);
            if (_inventory == null)return;
            
            _event.WhenSelect.AddListener(pointerEvent =>
            {
                if (_inventory.ActiveRangeWeaponPrefab != null)
                {
                    _inventory.ActiveRangeWeaponPrefab.SetActive(false);
                }
                if (_inventory.ActiveMeleeWeaponPrefab != null)
                {
                    _inventory.ActiveMeleeWeaponPrefab.SetActive(false);
                }
                
                
            });
        }

        private void LoadMrScene(PointerEvent pointerEvent)
        {
            OWNSceneManager.Instance.LoadMRScene();
        }
    }
}
