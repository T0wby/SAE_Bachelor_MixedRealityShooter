using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using UnityEngine;
using Utility;

public class MRPreperationBuilder : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _cubePrefab;
    private GameObject _currCube;
    

    [Header("Raycast Logic")]
    [SerializeField] private GameObject _rightControllerVisual;
    [SerializeField] private RayInteractor _rightController;
    [Tooltip("Number of the Layer that should be hit")]
    [SerializeField] private int _layerMaskNum = 6;
    private int _layerMask;

    private int _scaleNumber = 0;
    private int _rotationNumber = 0;
    private EColliderState _colliderState = EColliderState.Position; 
    private bool _isBuilding = true;

    private void Awake()
    {
        _layerMask = 1 << _layerMaskNum;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChange.AddListener(CheckGameState);
    }

    private void FixedUpdate()
    {
        if (!_isBuilding)return;
    
        SearchForPoint();
        
    }

    private void CheckGameState()
    {
        _isBuilding = GameManager.Instance.CurrState == EGameStates.PrepareMRScene;
    }

    private void SearchForPoint()
    {
        if (_colliderState != EColliderState.Position)return;
        
        if (Physics.Raycast(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward, out var hit, Mathf.Infinity, _layerMask))
        {
            Debug.DrawRay(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward * hit.distance, Color.green);
            if (_currCube == null)
                _currCube = Instantiate(_cubePrefab, hit.transform.position, Quaternion.identity);
            _currCube.transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(_rightControllerVisual.transform.position, _rightControllerVisual.transform.forward * 1000, Color.red);
            if (_currCube != null)
                Destroy(_currCube);
        }
    }

    private void RotateCurrCube()
    {
        if (_colliderState != EColliderState.Rotation)return;
    }
    
    private void ScaleCurrCube()
    {
        if (_colliderState != EColliderState.Scale)return;
    }
}
