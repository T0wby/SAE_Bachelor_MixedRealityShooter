using System;
using Manager;
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
        [SerializeField] private TMP_Text _healthPercentRight;
        private Material _matRight;
        [Header("PlayerMoney")]
        [SerializeField] private TMP_Text _moneyValueTextRight;
        private PlayerInventory _playerInventory;
        [Header("RoundStats")]
        [SerializeField] private TMP_Text _roundCounter;
        [SerializeField] private TMP_Text _enemyCounter;
        private int _maxRounds;
        private WaveManager _waveManager;

        private void Awake()
        {
            _matRight = _rendererRight.material;
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _waveManager = FindObjectOfType<WaveManager>();
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

            if (_waveManager != null)
            {
                _waveManager.onWaveChange.AddListener(UpdateRoundInformation);
                _waveManager.onEnemyRemoved.AddListener(UpdateEnemyInformation);
            }
            
            _maxRounds = GameManager.Instance.MaxRounds;

            UpdateRoundInformation(1);
            UpdateEnemyInformation(0);
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            var percent = newHealthValue / (float)_player.MaxHealth;
            _matRight.SetFloat("_Cutoff", 1f - percent);
            _healthPercentRight.text = $"{(int)percent * 100}%";
        }

        private void UpdateMoneyText(int newMoneyValue)
        {
            _moneyValueTextRight.text = $"{newMoneyValue}$";
        }

        private void UpdateRoundInformation(int currRound)
        {
            _roundCounter.text = $"{currRound}/{_maxRounds}";
        }

        private void UpdateEnemyInformation(int defeatedEnemyCount)
        {
            if (_waveManager == null)return;
            
            _enemyCounter.text = $"{defeatedEnemyCount}/{_waveManager.CurrRoundEnemySpawnCount}";
        }
    }
}
