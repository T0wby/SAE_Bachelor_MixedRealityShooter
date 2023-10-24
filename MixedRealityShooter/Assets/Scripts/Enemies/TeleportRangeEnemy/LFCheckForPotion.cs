using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckForPotion : Node
    {
        private EnemyTP _enemyTp;
        public LFCheckForPotion(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }

        public override ENodeState CalculateState()
        {
            Debug.Log("LFCheckForPotion");
            return _enemyTp.HealthPotionAmount > 0 ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
