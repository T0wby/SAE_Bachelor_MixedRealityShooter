using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class SwitchSceneTest : MonoBehaviour
{
    public void ButtonLogic()
    {
        OWNSceneManager.Instance.LoadMRScene();
    }
}
