using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    private WallDissolve[] _dissolveControllers;
    // Start is called before the first frame update
    void Start()
    {
        _dissolveControllers = FindObjectsByType<WallDissolve>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (var entity in _dissolveControllers)
            {
                entity.DissolveMaterial(4.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var entity in _dissolveControllers)
            {
                entity.ReturnMaterial(2.0f);
            }
        }
    }
}
