using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private InputAction _interact;

        public UnityEvent OnInteraction;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _interact = _playerControls.Player.Interact;
            _interact.Enable();
        }

        private void OnDisable()
        {
            _interact.Disable();
        }
        
        public void Interact(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                // TODO: Connect Interact and firering a weapon that is grabbed
                Debug.Log("Interact!");
                OnInteraction.Invoke();
            }
        }
    }
}
