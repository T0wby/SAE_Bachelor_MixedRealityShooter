using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckAttackTimer : Node
    {
        private Enemy _enemy;
        public LFCheckAttackTimer(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            return _enemy.IsAttacking ? ENodeState.FAILURE : ENodeState.SUCCESS;
        }
    }
}
