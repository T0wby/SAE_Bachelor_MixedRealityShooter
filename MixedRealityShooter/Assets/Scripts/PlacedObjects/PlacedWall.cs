using System;
using System.Collections.Generic;
using Manager;
using Player;
using UnityEngine;
using Utility;

namespace PlacedObjects
{
    public class PlacedWall : APlacedObject
    {
        [Header("AI Spawn Points")]
        [SerializeField] private List<Transform> _aiSpawns;
        private PlayerDamageHandler _player;

        public Transform Spawn { get; private set; }

        private void Start()
        {
            _player = FindObjectOfType<PlayerDamageHandler>();
            if (_player == null)return;
            SetNearestSpawnPoint();
            GameManager.Instance.onGameStateChange.AddListener(DisableChildObjects);
        }

        private void SetNearestSpawnPoint()
        {
            float closestDist = float.MaxValue;
            foreach (var point in _aiSpawns)
            {
                var dist = (point.position - _player.transform.position).sqrMagnitude;
                if (!(dist < closestDist)) continue;
                closestDist = dist;
                Spawn = point;
            }
        }

        private void DisableChildObjects(EGameStates currState)
        {
            if (currState != EGameStates.InHub)return;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
