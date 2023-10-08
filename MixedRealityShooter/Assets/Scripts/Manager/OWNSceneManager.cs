using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Manager
{
    public class OWNSceneManager : Singleton<OWNSceneManager>
    {
        public void LoadVRScene()
        {
            Debug.LogWarning("SwitchScene");
            SceneManager.LoadScene(1);
        }
        
        public void LoadMRScene()
        {
            Debug.LogWarning("SwitchScene");
            SceneManager.LoadScene(0);
        }
    }
}
