using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private InputAction _interact;

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
                Debug.Log("Interact!");
            }
        }
    }
}
