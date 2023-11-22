using System;
using Player;
using UnityEngine;

namespace UI
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private Transform _camera;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            if (_cameraPoint != null && _camera != null && _playerController != null)
                _playerController.onSecondaryButton.AddListener(UpdatePos);
        }

        private void OnDisable()
        {
            if (_playerController != null)
                _playerController.onSecondaryButton.RemoveListener(UpdatePos);
        }

        private void UpdatePos()
        {
            transform.position = new Vector3(_cameraPoint.position.x, 1.5f, _cameraPoint.position.z);
            transform.localRotation = Quaternion.LookRotation(transform.position - _camera.position, Vector3.up);
        }
    }
}
