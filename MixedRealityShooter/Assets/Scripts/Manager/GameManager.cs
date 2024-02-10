using System;
using System.Collections.Generic;
using System.Linq;
using PlacedObjects;
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
        public UnityEvent<EGameStates> onGameStateChange;

        #endregion

        #region Properties

        public EGameStates CurrState
        {
            get => _currState;
            set
            {
                _currState = value;
                onGameStateChange.Invoke(_currState);
            }
        }

        public List<GameObject> MrPlacedObjects => _mrPlacedObjects;
        public int CurrRound => _currRound;

        public int MaxRounds { get; set; }

        #endregion


        private void Start()
        {
            _mrPlacedObjects = new List<GameObject>();
            onGameStateChange.AddListener(SwitchObjVisibility);
            onGameStateChange.AddListener(DestroyPlacedVrObjects);
        }
        
        public void StartRound()
        {
            foreach (var placedObj in _mrPlacedObjects.Where(obj => obj != null))
            {
                placedObj.isStatic = true;
            }

            _currRound++;
            CurrState = EGameStates.InGame;
        }

        public void CheckIfRoundIsOver(int livingEnemyCount, int enemiesLeftToSpawn)
        {
            if (livingEnemyCount > 0 || enemiesLeftToSpawn > 0) return;

            if (_currRound + 1 > MaxRounds)
            {
                CurrState = EGameStates.GameDone;
                return;
            }

            foreach (var placedObj in _mrPlacedObjects.Where(obj => obj != null))
            {
                placedObj.isStatic = false;
            }

            CurrState = EGameStates.RoundOver;
        }

        private void ChangeMrObjectStatus(bool statusToChangeTo)
        {
            foreach (var obj in _mrPlacedObjects)
            {
                if (obj == null) continue;

                obj.SetActive(statusToChangeTo);
            }
        }

        private void ChangeToGameMaterial()
        {
            foreach (var obj in _mrPlacedObjects)
            {
                if (obj == null || obj.CompareTag("InvenObj")) continue;

                if (obj.CompareTag("Wall"))
                {
                    var placedWall = obj.GetComponent<APlacedObject>();
                    if (placedWall == null) continue;
                    placedWall.SetGameColor();
                }

                if (!obj.CompareTag("PlacedObj")) continue;
                if (obj.transform.childCount == 0) continue;
                var placedObj = obj.transform.GetChild(0).GetComponent<APlacedObject>();
                if (placedObj == null) continue;
                placedObj.SetGameColor();
            }
        }

        private void DestroyPlacedVrObjects(EGameStates state)
        {
            if (state != EGameStates.GameOver) return;
            foreach (var obj in _mrPlacedObjects)
            {
                if (!obj.CompareTag("InvenObj")) continue;
                Destroy(obj);
            }

            _currRound = 0;
            _mrPlacedObjects.RemoveAll(obj => obj == null);
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
                    ChangeToGameMaterial();
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
                case EGameStates.RoundOver:
                    break;
                case EGameStates.GameDone:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}