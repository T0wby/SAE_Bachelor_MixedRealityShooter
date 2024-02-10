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

            _pos = _enemy.WeaponTransform.position;

            var dir = (_enemy.PlayerTransform.position - _pos).normalized;
            _angle = Vector3.Angle(_enemy.transform.TransformDirection(_enemy.PlayerTransform.forward), dir);

            if (!(_angle <= _settings.FOV)) return false;
            if (Physics.Raycast(_pos, dir, out var hit, _settings.AttackRange + 1.0f, _enemy.IgnoreLayer))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.DrawRay(_pos, dir, Color.green, 1.0f);
                    return true;
                }
                Debug.DrawRay(_pos, dir, Color.red, 1.0f);
                return false;
            }

            return false;
        }
    }
}