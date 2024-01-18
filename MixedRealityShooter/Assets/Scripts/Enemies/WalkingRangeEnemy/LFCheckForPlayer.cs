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

            var dir = (_enemy.PlayerTransform.position - _pos).normalized;
            _angle = Vector3.Angle(_enemy.PlayerTransform.forward, dir);

            if (!(_angle <= _settings.FOV)) return false;
            Debug.DrawRay(_pos, dir, Color.green, 1.0f);
            var plhit = Physics.Raycast(_pos, dir, out var hit, _settings.AttackRange + 1.0f, _enemy.IgnoreLayer) && hit.transform.CompareTag("Player");
            return plhit;
        }
    }
}
