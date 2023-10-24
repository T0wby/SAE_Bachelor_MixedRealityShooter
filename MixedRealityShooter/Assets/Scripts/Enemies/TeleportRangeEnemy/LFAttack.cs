using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFAttack : Node
    {
        private EnemyTP _enemyTp;
        
        public LFAttack(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }
        
        public override ENodeState CalculateState()
        {
            Debug.Log("LFAttack");
            _enemyTp.StartAttack();
            
            return ENodeState.SUCCESS;
        }
    }
}
