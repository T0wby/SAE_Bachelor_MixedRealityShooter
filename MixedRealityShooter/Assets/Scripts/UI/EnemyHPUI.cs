using System;
using Enemies;
using UnityEngine;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    public class EnemyHPUI : MonoBehaviour
    {
        [FormerlySerializedAs("_enemy")] [SerializeField] private EnemyTP enemyTp;
        [SerializeField] private Slider _slider;

        private void Awake()
        {
            enemyTp.OnHealthChange.AddListener(UpdateHealthBar);
        }

        private void UpdateHealthBar(int newHealthValue)
        {
            _slider.value = newHealthValue / (float)enemyTp.Settings.Health;
        }
    }
}
