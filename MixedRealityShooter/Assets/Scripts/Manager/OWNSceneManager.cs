using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Manager
{
    public class OWNSceneManager : Singleton<OWNSceneManager>
    {
        public void LoadVRScene()
        {
            SceneManager.LoadScene(1);
        }
        
        public void LoadMRScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
