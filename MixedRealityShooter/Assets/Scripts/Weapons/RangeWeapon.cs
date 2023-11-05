using System;
using Oculus.Interaction;
using UnityEngine;
using Utility;
using Player;
using Projectile;

namespace Weapons
{
    public class RangeWeapon : AWeapon
    {
        [Header("Other")]
        [SerializeField] private GameObject _barrel;
        [Tooltip("Number of the Layer that should be ignored")]
        [SerializeField] private int _layerMaskNum = 8;
        private PlayerController _playerController;
        private ProjectilePool _projectilePool;
        private int _layerMask;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _projectilePool = FindObjectOfType<ProjectilePool>();
            if (_playerController != null)
            {
                _playerController.OnInteraction.AddListener(FireWeaponProjectile);
            }
            _layerMask = 1 << _layerMaskNum;
            
            // Invert bitmask
            _layerMask = ~_layerMask;
        }

        private void OnEnable()
        {
            _playerController = FindObjectOfType<PlayerController>();

            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<ProjectilePool>();
            }
            
            if (_playerController != null)
            {
                _playerController.OnInteraction.AddListener(FireWeaponProjectile);
            }
            _layerMask = 1 << _layerMaskNum;
            
            // Invert bitmask
            _layerMask = ~_layerMask;
            _isGrabbed = false;
        }

        /// <summary>
        /// Fire Weapon using a Raycast
        /// </summary>
        public void FireWeapon()
        {
            if(!_isGrabbed)return;
            
            if (Physics.Raycast(_barrel.transform.position, _barrel.transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, _layerMask))
            {
                IDamage hitObject = hit.collider.gameObject.GetComponent<IDamage>();
                if (hitObject != null)
                {
                    hitObject.TakeDamage(_damage);
                    Debug.Log("Did Hit");
                }
            }
            
            Debug.Log("Fire");
        }
        
        /// <summary>
        /// Fire Weapon using a projectile
        /// </summary>
        public void FireWeaponProjectile()
        {
            if(!_isGrabbed)return;
            if(_projectilePool == null)return;

            var tmp = _projectilePool.ArPool.GetItem();
            tmp.InitProjectileStats(_defaultSettings.Damage);
            tmp.transform.position = _barrel.transform.position;
            tmp.ThisRb.AddForce(_barrel.transform.forward * _projectileSpeed, ForceMode.Impulse);
        }

        public override void Attack()
        {
            if(_projectilePool == null)return;

            var tmp = _projectilePool.ArPool.GetItem();
            tmp.InitProjectileStats(_defaultSettings.Damage);
            tmp.transform.position = _barrel.transform.position;
            tmp.ThisRb.AddForce(_barrel.transform.forward * _projectileSpeed, ForceMode.Impulse);
        }
    }
}
