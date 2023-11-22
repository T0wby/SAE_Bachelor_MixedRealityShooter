using Manager;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(GameManager.Instance.StartRound);
        }
    }
}
