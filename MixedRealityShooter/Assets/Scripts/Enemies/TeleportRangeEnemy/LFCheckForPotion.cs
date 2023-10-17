using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckForPotion : Node
    {
        private Enemy _enemy;
        public LFCheckForPotion(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            return _enemy.HealthPotionAmount > 0 ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
