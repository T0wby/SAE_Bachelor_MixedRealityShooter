using Manager;
using Oculus.Interaction;
using UnityEngine;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private PointableUnityEventWrapper _pointableUnityEvent;

        private void Start()
        {
            _pointableUnityEvent.WhenSelect.AddListener(GameManager.Instance.StartRound);
        }
    }
}
