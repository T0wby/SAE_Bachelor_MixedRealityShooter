using System;
using System.Collections;
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
        private bool _canFire = false;
        private bool _isShooting = false;

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _projectilePool = FindObjectOfType<ProjectilePool>();
            
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
            _layerMask = 1 << _layerMaskNum;
            
            // Invert bitmask
            _layerMask = ~_layerMask;
            _isGrabbed = false;
        }

        private void Update()
        {
            FireWeaponProjectile();
        }

        /// <summary>
        /// Fire Weapon using a Raycast
        /// </summary>
        public void FireWeaponRay()
        {
            if(!_isGrabbed)return;
            
            if (Physics.Raycast(_barrel.transform.position, _barrel.transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, _layerMask))
            {
                IDamage hitObject = hit.collider.gameObject.GetComponent<IDamage>();
                hitObject?.TakeDamage(_damage);
            }
        }
        
        /// <summary>
        /// Fire Weapon using a projectile
        /// </summary>
        private void FireWeaponProjectile()
        {
            if (!_canFire) return;
            if (_isShooting) return;
            if(_projectilePool == null)return;
            _isShooting = true;
            StartCoroutine(FireWeapon());
        }

        private IEnumerator FireWeapon()
        {
            var tmp = _projectilePool.ArPool.GetItem();
            tmp.InitProjectileStats(_damage);
            tmp.transform.position = _barrel.transform.position;
            tmp.ThisRb.AddForce(_barrel.transform.forward * _projectileSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(1/_bulletsPerSecond);
            _isShooting = false;
        }

        public override void Attack()
        {
            if(_projectilePool == null)return;

            var tmp = _projectilePool.ArPool.GetItem();
            tmp.InitProjectileStats(_damage);
            tmp.transform.position = _barrel.transform.position;
            tmp.ThisRb.AddForce(_barrel.transform.forward * _projectileSpeed, ForceMode.Impulse);
        }

        protected override void OnGrabbed(GrabInteractor interactor)
        {
            base.OnGrabbed(interactor);
            if (_playerController == null)return;
            _playerController.onFireWeapon.AddListener(EnableWeaponFire);
            _playerController.onCancelFireWeapon.AddListener(DisableWeaponFire);
        }

        protected override void OnReleased(GrabInteractor interactor)
        {
            base.OnReleased(interactor);
            if (_playerController == null)return;
            _playerController.onFireWeapon.RemoveListener(EnableWeaponFire);
            _playerController.onCancelFireWeapon.RemoveListener(DisableWeaponFire);
            
            // Disable shooting only if the last hand lets go of the weapon(counted as one)
            if (_grabInteractable.Interactors.Count > 1)return;
            _canFire = false;
        }

        private void EnableWeaponFire()
        {
            _canFire = true;
        }
        private void DisableWeaponFire()
        {
            _canFire = false;
        }
    }
}
