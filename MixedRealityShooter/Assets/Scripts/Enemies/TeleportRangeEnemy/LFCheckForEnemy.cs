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
            Debug.Log("LFCheckForEnemy");
            return _enemyTp.CheckForPlayerInSight() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
