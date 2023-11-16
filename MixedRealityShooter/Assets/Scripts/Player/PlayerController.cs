using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private InputAction _interact;
        private InputAction _fireWeapon;
        private InputAction _rotateScale;
        private InputAction _switchRotateScale;
        private InputAction _placeObj;
        private InputAction _resetView;

        public UnityEvent onInteraction;
        public UnityEvent onFireWeapon;
        public UnityEvent onCancelFireWeapon;
        public UnityEvent onSecondaryButton;
        public UnityEvent onPrimaryButton;
        public UnityEvent onThumbstickClick;
        public UnityEvent<Vector2> OnRotation;
        public UnityEvent<Vector2> OnScale;
        
        private Vector2 _thumbstickValue = Vector2.zero;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _interact = _playerControls.Player.Interact;
            _interact.Enable();
            _rotateScale = _playerControls.Player.RotateAndScale;
            _rotateScale.Enable();
            _switchRotateScale = _playerControls.Player.SwitchRotScale;
            _switchRotateScale.Enable();
            _placeObj = _playerControls.Player.PlaceObj;
            _placeObj.Enable();
            _fireWeapon = _playerControls.Player.FireWeapon;
            _fireWeapon.Enable();
            _resetView = _playerControls.Player.ResetView;
            _resetView.Enable();
        }

        private void OnDisable()
        {
            _interact.Disable();
            _rotateScale.Disable();
            _switchRotateScale.Disable();
            _placeObj.Disable();
            _fireWeapon.Disable();
            _resetView.Disable();
        }

        public void Interact(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onInteraction.Invoke();
            }
        }
        public void OnSecondaryButton(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onSecondaryButton.Invoke();
            }
        }
        public void OnPrimaryButton(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onPrimaryButton.Invoke();
            }
        }
        public void OnThumbstickClicked(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                onThumbstickClick.Invoke();
            }
        }
        
        public void OnThumbstick(InputAction.CallbackContext context)
        {
            _thumbstickValue = context.ReadValue<Vector2>();
            OnRotation.Invoke(_thumbstickValue);
            OnScale.Invoke(_thumbstickValue);
        }
        
        /// <summary>
        /// Action on release that is why we cancel on performed
        /// </summary>
        /// <param name="context"></param>
        public void FireWeapon(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                onFireWeapon.Invoke();
            }
            if(context.performed)
            {
                onCancelFireWeapon.Invoke();
            }
        }
    }
}
