using System;
using System.Collections.Generic;
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
        public UnityEvent OnGameStateChange;

        #endregion

        #region Properties

        public EGameStates CurrState
        {
            get => _currState;
            set
            {
                _currState = value;
                OnGameStateChange.Invoke();
            }
        }

        public List<GameObject> MrPlacedObjects => _mrPlacedObjects;

        #endregion


        private void Start()
        {
            _mrPlacedObjects = new List<GameObject>();
            OnGameStateChange.AddListener(SwitchObjVisibility);
        }

        private void ChangeMrObjectStatus(bool statusToChangeTo)
        {
            foreach (var obj in _mrPlacedObjects)
            {
                if(obj == null) continue;
                
                obj.SetActive(statusToChangeTo);
            }
        }

        private void SwitchObjVisibility()
        {
            switch (_currState)
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
