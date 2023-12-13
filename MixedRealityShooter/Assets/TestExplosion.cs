using System;
using System.Collections;
using System.Collections.Generic;
using Items.Barrel;
using Unity.VisualScripting;
using UnityEngine;

public class TestExplosion : MonoBehaviour
{
    [SerializeField] private ExplosionBarrel _barrel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _barrel.AddExplosionForce();
        }
    }
}
