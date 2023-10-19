using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFTeleport : Node
    {
        private Enemy _enemy;
        public LFTeleport(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            if (_enemy.Destination != null)
            {
                _enemy.transform.position = _enemy.Destination.position;
                return ENodeState.SUCCESS;
            }

            return ENodeState.FAILURE;
        }
    }
}
