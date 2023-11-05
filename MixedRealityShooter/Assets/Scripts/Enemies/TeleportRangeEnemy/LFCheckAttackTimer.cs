using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckAttackTimer : Node
    {
        private EnemyTP _enemyTp;
        public LFCheckAttackTimer(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }

        public override ENodeState CalculateState()
        {
            Debug.Log("LFCheckAttackTimer");
            return _enemyTp.IsAttacking ? ENodeState.FAILURE : ENodeState.SUCCESS;
        }
    }
}
