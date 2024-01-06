using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Utility;
using Random = UnityEngine.Random;

namespace Items.Barrel
{
    public class ExplosionBarrel : MonoBehaviour, IDamage
    {
        [Header("Collections")]
        [SerializeField] private ItemTargetCollector _collector;
        
        [Header("ExplosionSettings")]
        [SerializeField] private GameObject _originalObj;
        [SerializeField] private CapsuleCollider _originalCollider;
        [SerializeField] private GameObject _fracturedObj;
        [SerializeField] private VisualEffect _explosionVFX;
        [SerializeField] private int _explosionDamage = 30;
        [SerializeField] private float _explosionMinForce = 5;
        [SerializeField] private float _explosionMaxForce = 100;
        [SerializeField] private float _explosionForceRadius = 2;
        [SerializeField] private float _fractureDisappearTimer = 5.0f;


        public void TakeDamage(int damage)
        {
            //Trigger VFX and List of Damageable Items
            AddExplosionForce();
            _explosionVFX.Play();
            foreach (var target in _collector.TargetsInRange)
            {
                target.TakeDamage(_explosionDamage);
            }
        }

        public void AddExplosionForce()
        {
            _originalCollider.enabled = false;
            _originalObj.SetActive(false);
            _fracturedObj.SetActive(true);
            foreach (Transform tform in _fracturedObj.transform)
            {
                var rb = tform.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(Random.Range(_explosionMinForce, _explosionMaxForce), transform.position, _explosionForceRadius);
            }

            StartCoroutine(DisableFractures());
        }

        IEnumerator DisableFractures()
        {
            yield return new WaitForSeconds(_fractureDisappearTimer);
            _fracturedObj.SetActive(false);
            yield return null;
        }
    }
}
