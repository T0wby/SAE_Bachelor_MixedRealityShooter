using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Items.Barrel
{
    [RequireComponent(typeof(SphereCollider))]
    public class ItemTargetCollector : MonoBehaviour
    {
        [SerializeField] private float _collectionRange = 1.3f;
        private List<IDamage> _targetsInRange;
        public List<IDamage> TargetsInRange => _targetsInRange;

        private void OnEnable()
        {
            var sphere = GetComponent<SphereCollider>();
            _targetsInRange = new List<IDamage>();
            sphere.radius = _collectionRange;
            sphere.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<IDamage>();
            if (target == null || other.CompareTag("Projectile")) return;
            _targetsInRange.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<IDamage>();
            if (target == null) return;
            if (!_targetsInRange.Contains(target))return;
            _targetsInRange.Remove(target);
        }
    }
}
