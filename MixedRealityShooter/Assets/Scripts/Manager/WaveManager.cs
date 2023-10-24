using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.Events;
using Utility;
using Waves;
using Random = UnityEngine.Random;

namespace Manager
{
    public class WaveManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<WaveSettings> _settings;
        private int _currWaveNumb = 0;
        private List<AEnemy> _enemiesAlive = new List<AEnemy>();
        private EnemyPool[] _enemyPools;
        private EnemyFactory _enemyFactory;

        #endregion

        #region Properties

        public int CurrWaveNumb
        {
            get => _currWaveNumb;
            set
            {
                if (_currWaveNumb != value)
                {
                    _currWaveNumb = value;
                    OnWaveChange.Invoke(_currWaveNumb);
                }
            }
        }
        public List<AEnemy> EnemiesAlive => _enemiesAlive;

        #endregion

        #region Events

        public UnityEvent<int> OnWaveChange;
        public UnityEvent<int> OnEnemyCountChange;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnWaveChange.AddListener(SpawnWave);
            OnEnemyCountChange.AddListener(GameManager.Instance.CheckIfRoundIsOver);
        }

        private void Start()
        {
            GameManager.Instance.OnGameStateChange.AddListener(StartWaves);
            _enemyPools = FindObjectsByType<EnemyPool>(FindObjectsInactive.Include,FindObjectsSortMode.None);
            _enemyFactory = new EnemyFactory(_enemyPools);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameStateChange.RemoveListener(StartWaves);
        }

        #endregion

        #region Wave Methods
        /// <summary>
        /// Sets the Wave/Round number and triggers connected events via the used Property
        /// (Only triggers if the number is different to the current one and if the current game state is correct)
        /// </summary>
        /// <param name="state">The state that the game currently is in</param>
        private void StartWaves(EGameStates state)
        {
            if (state != EGameStates.InGame)return;
            
            CurrWaveNumb = GameManager.Instance.CurrRound;
        }

        /// <summary>
        /// Activates Enemies according to the specified Amount, but randomized for the available types
        /// </summary>
        /// <param name="currWave">Number of the wave to spawn</param>
        private void SpawnWave(int currWave)
        {
            if (currWave > _settings.Count) return;

            for (int i = 0; i < _settings[currWave - 1].EnemyAmount; i++)
            {
                var ran = Random.Range(0, _settings[currWave - 1].EnemyTypes.Count);
                var tmp = _enemyFactory.CreateEnemy(_settings[currWave - 1].EnemyTypes[ran]);
                tmp.WaveManager = this;
                AddLivingEnemy(tmp);
            }
        }

        #endregion

        private void AddLivingEnemy(AEnemy enemyToAdd)
        {
            _enemiesAlive.Add(enemyToAdd);
            OnEnemyCountChange.Invoke(_enemiesAlive.Count);
        }
        public void RemoveDeadEnemy(AEnemy enemyToRemove)
        {
            _enemiesAlive.Remove(enemyToRemove);
            OnEnemyCountChange.Invoke(_enemiesAlive.Count);
        }
    }
}
