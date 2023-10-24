using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckForEnemy : Node
    {
        private EnemyTP _enemyTp;
        
        public LFCheckForEnemy(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }

        public override ENodeState CalculateState()
        {
            var tmp = _enemyTp.CheckForPlayerInSight();
            Debug.Log($"LFCheckForEnemy: {tmp}");

            return tmp ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
