using Manager;
using Oculus.Interaction;
using UnityEngine;

namespace UI
{
    public class ButtonToMR : MonoBehaviour
    {
        private PointableUnityEventWrapper _event;

        private void Awake()
        {
            _event = GetComponent<PointableUnityEventWrapper>();
            if (_event == null) return;
        
            _event.WhenSelect.AddListener(LoadMrScene);
        }

        private void LoadMrScene(PointerEvent pointerEvent)
        {
            OWNSceneManager.Instance.LoadMRScene();
        }
    }
}
