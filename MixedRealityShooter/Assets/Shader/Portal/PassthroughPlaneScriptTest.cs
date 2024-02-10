using UnityEngine;

namespace Materials.Shader.Portal
{
    public class PassthroughPlaneScriptTest : MonoBehaviour
    {
        void Start()
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
            OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
#endif
        }
    }
}
