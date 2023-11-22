using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    

    // Update is called once per frame
    void Update()
    {
        _surface.BuildNavMesh();
    }
}
