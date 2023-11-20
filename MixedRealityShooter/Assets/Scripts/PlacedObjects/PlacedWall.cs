using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

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
        
    }
}
