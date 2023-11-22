using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Manager
{
    public class OWNSceneManager : Singleton<OWNSceneManager>
    {
        public void LoadVRScene(PointerEvent pointerEvent)
        {
            SceneManager.LoadScene(2);
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        public void LoadVRScene()
        {
            SceneManager.LoadScene(2);
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        
        public void LoadVRSceneFirst()
        {
            AnchorManager.Instance.SaveAllAnchors();
            SceneManager.LoadScene(2);
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        
        public void LoadMRScene()
        {
            SceneManager.LoadScene(1);
            GameManager.Instance.CurrState = EGameStates.PreparePlayScene;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
