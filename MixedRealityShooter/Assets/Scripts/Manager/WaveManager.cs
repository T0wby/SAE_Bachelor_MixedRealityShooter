using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
        private int _enemiesLeftToSpawn = 0;
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
                    onWaveChange.Invoke(_currWaveNumb);
                }
            }
        }
        public List<AEnemy> EnemiesAlive => _enemiesAlive;

        #endregion

        #region Events

        public UnityEvent<int> onWaveChange;
        public UnityEvent<int, int> onEnemyCountChange;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            onWaveChange.AddListener(SpawnWave);
        }

        private void Start()
        {
            onEnemyCountChange.AddListener(GameManager.Instance.CheckIfRoundIsOver);
            GameManager.Instance.onGameStateChange.AddListener(StartWaves);
            GameManager.Instance.onGameStateChange.AddListener(PlayerDeath);
            GameManager.Instance.MaxRounds = _settings.Count;
            _enemyPools = FindObjectsByType<EnemyPool>(FindObjectsInactive.Include,FindObjectsSortMode.None);
            _enemyFactory = new EnemyFactory(_enemyPools);
        }

        private void OnDestroy()
        {
            GameManager.Instance.onGameStateChange.RemoveListener(StartWaves);
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
            _enemiesLeftToSpawn = _settings[currWave - 1].EnemyAmount;
            StartCoroutine(SpawnWaveTimer(currWave));
        }

        private IEnumerator SpawnWaveTimer(int currWave)
        {
            // StartCountdown
            yield return new WaitForSeconds(_settings[currWave - 1].StartCountdown);

            for (int i = 0; i < _settings[currWave - 1].EnemyAmount; i++)
            {
                var ran = Random.Range(0, _settings[currWave - 1].EnemyTypes.Count);
                var tmp = _enemyFactory.CreateEnemy(_settings[currWave - 1].EnemyTypes[ran]);
                tmp.WaveManager = this;
                AddLivingEnemy(tmp);
                _enemiesLeftToSpawn--;
                // Time between spawns
                yield return new WaitForSeconds(_settings[currWave - 1].SpawnRate);
            }

            yield return null;
        }

        #endregion

        private void AddLivingEnemy(AEnemy enemyToAdd)
        {
            _enemiesAlive.Add(enemyToAdd);
            onEnemyCountChange.Invoke(_enemiesAlive.Count, _enemiesLeftToSpawn);
        }
        public void RemoveDeadEnemy(AEnemy enemyToRemove)
        {
            _enemiesAlive.Remove(enemyToRemove);
            onEnemyCountChange.Invoke(_enemiesAlive.Count, _enemiesLeftToSpawn);
        }

        private void PlayerDeath(EGameStates state)
        {
            if (state != EGameStates.GameOver)return;
            foreach (var enemy in _enemiesAlive)
            {
                enemy.ReturnEnemy();
            }
            
            _enemiesAlive.Clear();
        }
    }
}
