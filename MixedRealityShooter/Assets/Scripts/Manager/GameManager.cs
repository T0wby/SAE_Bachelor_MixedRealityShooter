using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Variables

        private EGameStates _currState = EGameStates.GameStart;
        private List<GameObject> _mrPlacedObjects;
        private int _currRound = 0;
        public UnityEvent<EGameStates> OnGameStateChange;
        
        private List<AEnemy> _enemiesAlive = new List<AEnemy>();
        #endregion

        #region Properties

        public EGameStates CurrState
        {
            get => _currState;
            set
            {
                _currState = value;
                OnGameStateChange.Invoke(_currState);
            }
        }

        public List<GameObject> MrPlacedObjects => _mrPlacedObjects;
        public List<AEnemy> EnemiesAlive => _enemiesAlive;
        public int CurrRound => _currRound;

        #endregion


        private void Start()
        {
            _mrPlacedObjects = new List<GameObject>();
            OnGameStateChange.AddListener(SwitchObjVisibility);
        }

        public void StartRound()
        {
            _currRound++;
            CurrState = EGameStates.InGame;
        }

        private void ChangeMrObjectStatus(bool statusToChangeTo)
        {
            foreach (var obj in _mrPlacedObjects)
            {
                if(obj == null) continue;
                
                obj.SetActive(statusToChangeTo);
            }
        }

        private void SwitchObjVisibility(EGameStates state)
        {
            switch (state)
            {
                case EGameStates.PrepareMRSceneWall:
                    break;
                case EGameStates.PrepareMRSceneInner:
                    break;
                case EGameStates.PreparePlayScene:
                    ChangeMrObjectStatus(true);
                    break;
                case EGameStates.InHub:
                    ChangeMrObjectStatus(false);
                    break;
                case EGameStates.InGame:
                    break;
                case EGameStates.GameOver:
                    break;
                case EGameStates.GameStart:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
