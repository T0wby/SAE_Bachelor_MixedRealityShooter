using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Waves
{
    [CreateAssetMenu(fileName = "NewWaveSettings", menuName = "Waves/Settings", order = 0)]
    public class WaveSettings : ScriptableObject
    {
        [SerializeField] private int _enemyAmount;
        [SerializeField] private List<EEnemyType> _enemyTypes;
        
        public int EnemyAmount => _enemyAmount;
        public  List<EEnemyType> EnemyTypes => _enemyTypes;
    }
}
