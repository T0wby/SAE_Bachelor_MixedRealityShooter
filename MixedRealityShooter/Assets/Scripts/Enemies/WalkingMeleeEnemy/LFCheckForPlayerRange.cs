using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.WalkingMeleeEnemy
{
    public class LFCheckForPlayerRange : Node
    {
        private readonly AEnemy _enemy;
        private readonly EnemySettings _settings;
        
        public LFCheckForPlayerRange(AEnemy enemy)
        {
            _enemy = enemy;
            _settings = enemy.Settings;
        }
        
        public override ENodeState CalculateState()
        {
            return CheckForPlayerInRange() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
        
        private bool CheckForPlayerInRange()
        {
            if (_enemy.PlayerTransform == null) return false;
            var dist = Vector3.Distance(_enemy.PlayerTransform.position, _enemy.transform.position);
            return dist < _settings.AttackRange;
        }
    }
}