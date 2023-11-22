using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFHeal : Node
    {
        private readonly AEnemy _enemy;
        
        public LFHeal(AEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public override ENodeState CalculateState()
        {
            _enemy.IsFleeing = false;
            _enemy.Heal();
            return ENodeState.SUCCESS;
        }
    }
}
