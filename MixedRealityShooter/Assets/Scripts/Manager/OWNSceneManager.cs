using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Manager
{
    public class OWNSceneManager : Singleton<OWNSceneManager>
    {
        public void LoadVRScene()
        {
            SceneManager.LoadScene(2);
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        
        public void LoadMRScene()
        {
            SceneManager.LoadScene(1);
            GameManager.Instance.CurrState = EGameStates.PreparePlayScene;
        }
    }
}
