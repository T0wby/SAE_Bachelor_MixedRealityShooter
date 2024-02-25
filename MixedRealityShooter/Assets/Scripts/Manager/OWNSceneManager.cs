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
            _dissolveController.ReturnDissolve();
        }
        public void LoadVRScene()
        {
            _dissolveController.ReturnDissolve();
        }
        
        public void LoadMRScene()
        {
            _dissolveController.StartDissolve();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
