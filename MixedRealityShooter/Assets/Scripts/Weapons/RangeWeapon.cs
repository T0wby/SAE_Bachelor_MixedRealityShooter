using Oculus.Interaction;
using UnityEngine;
using Utility;
using Player;
using Projectile;

namespace Weapons
{
    public class RangeWeapon : AWeapon
    {
        [SerializeField] private GameObject _barrel;
        [Tooltip("Number of the Layer that should be ignored")]
        [SerializeField] private int _layerMaskNum = 8;
        [SerializeField] private ActiveStateUnityEventWrapper _activeStateEvent;
        private PlayerController _playerController;
        private ProjectilePool _projectilePool;
        private int _layerMask;
        private bool _isGrabbed = false;

        private void Start()
        {
            _playerController = GameObject.FindObjectOfType<PlayerController>(); //TODO: Search for alternative
            _projectilePool = GameObject.FindObjectOfType<ProjectilePool>();
            if (_playerController != null)
            {
                //_playerController.OnInteraction.AddListener(FireWeapon);
                _playerController.OnInteraction.AddListener(FireWeaponProjectile);
            }
            _layerMask = 1 << _layerMaskNum;
            
            // Invert bitmask
            _layerMask = ~_layerMask;
            
            _activeStateEvent.WhenActivated.AddListener(OnGrabbed);
            _activeStateEvent.WhenDeactivated.AddListener(OnReleased);
        }

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
        
        public void FireWeaponProjectile()
        {
            if(!_isGrabbed)return;
            if(_projectilePool == null)return;

            var tmp = _projectilePool.ArPool.GetItem();
            tmp.transform.position = _barrel.transform.position;
            tmp.ThisRb.AddForce(_barrel.transform.forward * _projectileSpeed, ForceMode.Impulse);
        }

        public void OnGrabbed()
        {
            _isGrabbed = true;
        }
        public void OnReleased()
        {
            _isGrabbed = false;
        }
    }
}
