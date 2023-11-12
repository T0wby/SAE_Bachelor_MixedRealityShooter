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
        private InputAction _rotateScale;
        private InputAction _switchRotateScale;
        private InputAction _placeObj;

        public UnityEvent onInteraction;
        public UnityEvent onSecondaryButton;
        public UnityEvent onPrimaryButton;
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
        }

        private void OnDisable()
        {
            _interact.Disable();
            _rotateScale.Disable();
            _switchRotateScale.Disable();
            _placeObj.Disable();
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
        
        public void OnThumbstick(InputAction.CallbackContext context)
        {
            _thumbstickValue = context.ReadValue<Vector2>();
            OnRotation.Invoke(_thumbstickValue);
            OnScale.Invoke(_thumbstickValue);
        }
    }
}
