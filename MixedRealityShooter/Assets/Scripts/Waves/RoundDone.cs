using Manager;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Waves
{
    public class RoundDone : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OWNSceneManager.Instance.LoadVRScene);
        }
    }
}
