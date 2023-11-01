using Manager;
using Oculus.Interaction;
using UnityEngine;

namespace Waves
{
    public class RoundDone : MonoBehaviour
    {
        [SerializeField] private PointableUnityEventWrapper _pointableUnityEvent;

        private void OnEnable()
        {
            _pointableUnityEvent.WhenSelect.AddListener(OWNSceneManager.Instance.LoadVRScene);
        }
    }
}
