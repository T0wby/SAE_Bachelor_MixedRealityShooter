using System;
using Manager;
using Oculus.Interaction;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private GameObject _roundDoneObjs;
        [SerializeField] private GameObject _gameOverObjs;
        [SerializeField] private GameObject _gameDoneObjs;
        [SerializeField] private GameObject _ongoingRoundObjs;
        [SerializeField] private GameObject _anchorGameObject;
        
        [Header("NavMesh")]
        [SerializeField] private NavMeshSurface _surface;

        private void Start()
        {
            GameManager.Instance.onGameStateChange.AddListener(PrepareScene);
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
                    OngoingRoundObjs();
                    break;
                case EGameStates.GameOver:
                    GameOverPreparation();
                    break;
                case EGameStates.GameStart:
                    CheckIfAnchorsExist();
                    break;
                case EGameStates.RoundOver:
                    RoundOverPreparation();
                    break;
                case EGameStates.GameDone:
                    GameDonePreparation();
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
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(true);
        }
        private void MrInnerPreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(true);
        }
        private void GamePreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(true);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
        }
        private void GameOverPreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(true);
            _mrInsidePrepObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
        }
        private void GameDonePreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(true);
            _gameOverObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
        }
        private void RoundOverPreparation()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(true);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(false);
        }
        private void OngoingRoundObjs()
        {
            _startObjs.SetActive(false);
            _playPrepObjs.SetActive(false);
            _mrWallPrepObjs.SetActive(false);
            _roundDoneObjs.SetActive(false);
            _gameDoneObjs.SetActive(false);
            _gameOverObjs.SetActive(false);
            _mrInsidePrepObjs.SetActive(false);
            _ongoingRoundObjs.SetActive(true);
        }

        #region Event Methods

        public void ChangeToMrWallPrep()
        {
            GameManager.Instance.CurrState = EGameStates.PrepareMRSceneWall;
        }
        
        public void ChangeToMrInsidePrep()
        {
            GameManager.Instance.CurrState = EGameStates.PrepareMRSceneInner;
        }

        private void CheckIfAnchorsExist()
        {
            if (AnchorManager.Instance && AnchorManager.Instance.CheckForAnchors())
            {
                // Open UI to ask if the user wishes to use the anchors or not
                _anchorGameObject.SetActive(true);
            }
        }

        #endregion
    }
}
