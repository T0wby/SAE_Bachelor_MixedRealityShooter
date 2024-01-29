using System;
using Manager;
using UnityEngine;
using Utility;

namespace Player
{
    public class TutorialController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject _buildTutorial;
        [SerializeField] private GameObject _placeBoughtItemsTutorial;
        [SerializeField] private GameObject _leftHandIngameInfo;
        [SerializeField] private GameObject _leftHandBuildInfo;

        private void Start()
        {
            GameManager.Instance.onGameStateChange.AddListener(SetCurrentTutorial);
            SetCurrentTutorial(GameManager.Instance.CurrState);
        }

        private void SetCurrentTutorial(EGameStates currState)
        {
            switch (currState)
            {
                case EGameStates.PrepareMRSceneWall:
                    SetupBuildTutorial();
                    break;
                case EGameStates.PrepareMRSceneInner:
                    SetupBuildTutorial();
                    break;
                case EGameStates.PreparePlayScene:
                    SetupIngameTutorial();
                    break;
                case EGameStates.InHub:
                    SetupHubTutorial();
                    break;
                case EGameStates.InGame:
                    DeactivateTutorials();
                    break;
                case EGameStates.GameOver:
                    DeactivateTutorials();
                    break;
                case EGameStates.GameStart:
                    DeactivateTutorials();
                    break;
                case EGameStates.RoundOver:
                    DeactivateTutorials();
                    break;
            }
        }

        private void SetupBuildTutorial()
        {
            _buildTutorial.SetActive(true);
            _placeBoughtItemsTutorial.SetActive(false);
            _leftHandBuildInfo.SetActive(false);
        }
        private void SetupHubTutorial()
        {
            _buildTutorial.SetActive(false);
            _placeBoughtItemsTutorial.SetActive(false);
            _leftHandBuildInfo.SetActive(false);
        }
        private void SetupIngameTutorial()
        {
            _buildTutorial.SetActive(false);
            _leftHandIngameInfo.SetActive(false);
            _leftHandBuildInfo.SetActive(true);
            _placeBoughtItemsTutorial.SetActive(true);
        }
        private void DeactivateTutorials()
        {
            _buildTutorial.SetActive(false);
            _placeBoughtItemsTutorial.SetActive(false);
            _leftHandBuildInfo.SetActive(false);
            _leftHandIngameInfo.SetActive(true);
        }
    }
}
