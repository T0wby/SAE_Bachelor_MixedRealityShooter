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
            Debug.Log("LFTeleport");
            _enemy.StartTeleport();
            return ENodeState.SUCCESS;
        }
    }
}
