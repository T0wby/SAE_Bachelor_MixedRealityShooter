using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Weapons
{
    public class RangeWeapon : AWeapon
    {
        [SerializeField] private GameObject _barrel;
        [Tooltip("Number of the Layer that should be ignored")]
        [SerializeField] private int _layerMaskNum = 8;
        [SerializeField] private ActiveStateUnityEventWrapper _activeStateEvent;
        private int _layerMask;
        private bool _isGrabbed = false;

        private void Start()
        {
            _layerMask = 1 << _layerMaskNum;
            
            // Invert bitmask
            _layerMask = ~_layerMask;
            
            _activeStateEvent.WhenActivated.AddListener(OnGrabbed);
            _activeStateEvent.WhenDeactivated.AddListener(OnReleased);
        }

        public void FireWeapon()
        {
            if (Physics.Raycast(_barrel.transform.position, _barrel.transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, _layerMask))
            {
                IDamage hitObject = hit.collider.gameObject.GetComponent<IDamage>();
                if (hitObject != null)
                {
                    hitObject.TakeDamage(_damage);
                    Debug.Log("Did Hit");
                }
            }
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
