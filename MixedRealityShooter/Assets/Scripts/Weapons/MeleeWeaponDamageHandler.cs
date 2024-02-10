using System;
using UnityEngine;
using Utility;

namespace Weapons
{
    public class MeleeWeaponDamageHandler : MonoBehaviour
    {
        [SerializeField] private MeleeWeapon _owner;
        [SerializeField] private bool _damagePlayer = false;
        private IDamage _objToDamage;
        
        private void Start()
        {
            if (transform.parent != null && _owner == null)
            {
                _owner = transform.parent.GetComponent<MeleeWeapon>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_damagePlayer && other.CompareTag("Player")) return;
            _objToDamage = other.GetComponent<IDamage>();
            
            if (_objToDamage == null)return;
            
            _objToDamage.TakeDamage(_owner.CurrDamage);
            _objToDamage = null;
        }
    }
}
