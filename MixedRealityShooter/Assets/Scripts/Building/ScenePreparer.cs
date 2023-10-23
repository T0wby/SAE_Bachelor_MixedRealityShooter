using System;
using Manager;
using Oculus.Interaction;
using UnityEngine;
using Utility;

namespace Building
{
    public class ScenePreparer : MonoBehaviour
    {
        [Header("PreparationObjects")] 
        [SerializeField] private GameObject _mrWallPrepObjs;
        [SerializeField] private GameObject _mrInsidePrepObjs;
        [SerializeField] private GameObject _playPrepObjs;
        [SerializeField] private GameObject _startObjs;

        [Header("ButtonEvents")] 
        [SerializeField] private PointableUnityEventWrapper _eventsStartGameButton;
        [SerializeField] private PointableUnityEventWrapper _eventsInnerModeButton;

        private void Start()
        {
            GameManager.Instance.OnGameStateChange.AddListener(PrepareScene);
            _eventsStartGameButton.WhenSelect.AddListener(ChangeToMrWallPrep);
            _eventsInnerModeButton.WhenSelect.AddListener(ChangeToMrInsidePrep);
            PrepareScene(GameManager.Instance.CurrState);
        }

        private void PrepareScene(EGameStates state)
        {
            switch (state)
            {
                case EGameStates.PrepareMRSceneWall:
                    MrWallPreparation();
                    break;
                case EGameStates.PrepareMRSceneInner:
                    MrInnerPreparation();
                    break;
                case EGameStates.PreparePlayScene:
                    GamePreparation();
                    break;
                case EGameStates.InHub:
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
        private void MrWallPreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(true);
        }
        private void MrInnerPreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(true);
        }
        private void GamePreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(true);
            _mrWallPrepObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
        }

        #region Event Methods

        private void ChangeToMrWallPrep(PointerEvent pointerEvent)
        {
            GameManager.Instance.CurrState = EGameStates.PrepareMRSceneWall;
        }
        
        private void ChangeToMrInsidePrep(PointerEvent pointerEvent)
        {
            GameManager.Instance.CurrState = EGameStates.PrepareMRSceneInner;
        }

        #endregion
    }
}
