using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Player
{
    public class PlayerStatusUIHandler : MonoBehaviour
    {
        [SerializeField] private PlayerStatus _player;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _player.OnHealthChange.AddListener(UpdateHealthBar);
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            _slider.value = newHealthValue / (float)_player.MaxHealth;
        }
    }
}
