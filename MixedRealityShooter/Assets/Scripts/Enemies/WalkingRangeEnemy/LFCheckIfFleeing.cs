using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFCheckIfFleeing : Node
    {
        private readonly AEnemy _enemy;
        public LFCheckIfFleeing(AEnemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            if (_enemy.IsFleeing) return ENodeState.FAILURE;
            _enemy.IsFleeing = true;
            return ENodeState.SUCCESS;

        }
    }
}
