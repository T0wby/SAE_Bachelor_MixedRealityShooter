using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class ResetVrEnvironment : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _vrAreas;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindObjectOfType<PlayerController>();
            if (_playerController != null)
                _playerController.onSecondaryButton.AddListener(ResetPositions);
        }

        private void ResetPositions()
        {
            var pos = _playerController.transform.position;

            foreach (var area in _vrAreas.Where(area => area != null))
            {
                area.transform.position = new Vector3(pos.x, area.transform.position.y, pos.z);
            }
        }
    }
}
