using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFAttack : Node
    {
        private AEnemy _enemy;
        
        public LFAttack(AEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public override ENodeState CalculateState()
        {
            _enemy.Attack();
            return ENodeState.SUCCESS;
        }
    }
}
