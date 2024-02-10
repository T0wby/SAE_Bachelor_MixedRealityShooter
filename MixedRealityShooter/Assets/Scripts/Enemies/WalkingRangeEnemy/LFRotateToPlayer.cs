using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFRotateToPlayer : Node
    {
        private readonly AEnemy _enemy;

        public LFRotateToPlayer(AEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public override ENodeState CalculateState()
        {
            _enemy.transform.LookAt(_enemy.PlayerTransform.position);
            return ENodeState.SUCCESS;
        }
    }
}
