using System;
using Enemies;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    public class EnemyHPUI : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            _enemy.OnHealthChange.AddListener(UpdateHealthBar);
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            _slider.value = newHealthValue / (float)_enemy.Settings.Health;
        }
    }
}
