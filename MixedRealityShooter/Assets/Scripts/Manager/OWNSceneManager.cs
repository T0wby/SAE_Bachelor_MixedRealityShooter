using Oculus.Interaction;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Manager
{
    public class OWNSceneManager : Singleton<OWNSceneManager>
    {
        [SerializeField] private DissolveController _dissolveController;
        
        public void LoadVRScene(PointerEvent pointerEvent)
        {
            //SceneManager.LoadScene(2);
            _dissolveController.ReturnDissolve();
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        public void LoadVRScene()
        {
            //SceneManager.LoadScene(2);
            _dissolveController.ReturnDissolve();
            GameManager.Instance.CurrState = EGameStates.InHub;
        }
        
        public void LoadMRScene()
        {
            //SceneManager.LoadScene(1);
            _dissolveController.StartDissolve();
            GameManager.Instance.CurrState = EGameStates.PreparePlayScene;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
