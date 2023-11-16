using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFHeal : Node
    {
        private EnemyTP _enemyTpSelf;
        
        public LFHeal(EnemyTP enemyTpSelf)
        {
            _enemyTpSelf = enemyTpSelf;
        }
        
        public override ENodeState CalculateState()
        {
            Debug.LogWarning("LFHeal");
            _enemyTpSelf.Heal();
            return ENodeState.SUCCESS;
        }
    }
}
