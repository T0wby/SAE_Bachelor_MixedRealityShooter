using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFAttack : Node
    {
        private Enemy _enemy;
        
        public LFAttack(Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public override ENodeState CalculateState()
        {
            _enemy.StartAttack();
            
            return ENodeState.SUCCESS;
        }
    }
}
