using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using Utility;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private List<WallDissolve> _disolveObjs;
    [SerializeField] private List<GameObject> _toggleObjsVR;
    [SerializeField] private List<GameObject> _toggleObjsMR;
    [SerializeField] private GameObject _gamePrepObj;
    [SerializeField] private float _dissolveTime = 4.0f;

    public void StartDissolve()
    {
        StartCoroutine(BeginDissolving());
    }

    private IEnumerator BeginDissolving()
    {
        foreach (var entity in _toggleObjsVR)
        {
            entity.SetActive(false);
        }
        
        foreach (var entity in _disolveObjs)
        {
            entity.DissolveMaterial(_dissolveTime);
        }
        GameManager.Instance.CurrState = EGameStates.PreparePlayScene;
        yield return new WaitForSeconds(_dissolveTime + 0.5f);
        
        GameManager.Instance.CurrState = EGameStates.PreparePlayScene;
        _gamePrepObj.SetActive(true);
    }
    
    public void ReturnDissolve()
    {
        StartCoroutine(EndDissolving());
    }

    private IEnumerator EndDissolving()
    {
        foreach (var entity in _toggleObjsMR)
        {
            entity.SetActive(false);
        }
        
        foreach (var entity in _disolveObjs)
        {
            entity.ReturnMaterial(_dissolveTime);
        }

        yield return new WaitForSeconds(_dissolveTime + 0.5f);
        GameManager.Instance.CurrState = EGameStates.InHub;
        
        foreach (var entity in _toggleObjsVR)
        {
            entity.SetActive(true);
        }
    }
}
