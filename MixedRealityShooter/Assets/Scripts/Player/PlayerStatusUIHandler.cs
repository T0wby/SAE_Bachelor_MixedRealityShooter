using System;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Player
{
    public class PlayerStatusUIHandler : MonoBehaviour
    {
        [SerializeField] private PlayerStatus _player;
        [SerializeField] private Slider _slider;
        [SerializeField] private Renderer _rendererRight;
        [SerializeField] private Renderer _rendererLeft;
        private Material _matRight;
        private Material _matLeft;

        private void Awake()
        {
            _matRight = _rendererRight.material;
            _matLeft = _rendererLeft.material;
        }

        private void Start()
        {
            _player.OnHealthChange.AddListener(UpdateHealthBar);
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            //_slider.value = newHealthValue / (float)_player.MaxHealth;
            _matRight.SetFloat("_Cutoff", 1f - (newHealthValue / (float)_player.MaxHealth));
            _matLeft.SetFloat("_Cutoff", 1f - (newHealthValue / (float)_player.MaxHealth));
        }
    }
}
