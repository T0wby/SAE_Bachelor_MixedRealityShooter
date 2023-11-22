using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFCheckForPlayer : Node
    {
        private readonly AEnemy _enemy;
        private readonly EnemySettings _settings;
        private Vector3 _pos;
        private float _angle;
        
        public LFCheckForPlayer(AEnemy enemy)
        {
            _enemy = enemy;
            _settings = enemy.Settings;
        }
        
        public override ENodeState CalculateState()
        {
            return CheckForPlayerInSight() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
        
        private bool CheckForPlayerInSight()
        {
            if (_enemy.PlayerTransform == null) return false;

            _pos = _enemy.transform.position;
            _angle = Vector3.Angle(_pos, _enemy.PlayerTransform.position);

            if (!(_angle <= _settings.FOV)) return false;
            Vector3 dir = _enemy.PlayerTransform.transform.position - _enemy.transform.position;
            Debug.DrawRay(_pos, dir, Color.green, 1.0f);
            return Physics.Raycast(_pos, dir, out var hit, Mathf.Infinity, _enemy.IgnoreLayer) && hit.transform.CompareTag("Player");
        }
    }
}
