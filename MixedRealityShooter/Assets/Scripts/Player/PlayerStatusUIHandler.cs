using System;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Player
{
    public class PlayerStatusUIHandler : MonoBehaviour
    {
        [Header("PlayerHealth")]
        [SerializeField] private PlayerStatus _player;
        [SerializeField] private Renderer _rendererRight;
        [SerializeField] private Renderer _rendererLeft;
        private Material _matRight;
        private Material _matLeft;
        [Header("PlayerMoney")]
        [SerializeField] private TMP_Text _moneyValueTextLeft;
        [SerializeField] private TMP_Text _moneyValueTextRight;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            _matRight = _rendererRight.material;
            _matLeft = _rendererLeft.material;
            _playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Start()
        {
            if(_player != null)
                _player.onHealthChange.AddListener(UpdateHealthBar);
            if (_playerInventory != null)
            {
                _playerInventory.onMoneyChange.AddListener(UpdateMoneyText);
                _playerInventory.onMoneyChange.Invoke(_playerInventory.Money);
            }
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            //_slider.value = newHealthValue / (float)_player.MaxHealth;
            _matRight.SetFloat("_Cutoff", 1f - (newHealthValue / (float)_player.MaxHealth));
            _matLeft.SetFloat("_Cutoff", 1f - (newHealthValue / (float)_player.MaxHealth));
        }

        private void UpdateMoneyText(int newMoneyValue)
        {
            _moneyValueTextLeft.text = newMoneyValue.ToString();
            _moneyValueTextRight.text = newMoneyValue.ToString();
        }
    }
}
