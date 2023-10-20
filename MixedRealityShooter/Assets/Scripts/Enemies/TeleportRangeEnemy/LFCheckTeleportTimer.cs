using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckTeleportTimer : Node
    {
        private Enemy _enemy;
        public LFCheckTeleportTimer(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            return _enemy.CanMove ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
