using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Oculus.Interaction;
using UnityEngine;
using Utility;

public class StartOverallGame : MonoBehaviour
{
    [SerializeField] private PointableUnityEventWrapper _event;

    private void Awake()
    {
        _event.WhenSelect.AddListener(ChangeToMrPrep);
    }

    private void ChangeToMrPrep(PointerEvent pointerEvent)
    {
        GameManager.Instance.CurrState = EGameStates.PrepareMRScene;
    }
}
