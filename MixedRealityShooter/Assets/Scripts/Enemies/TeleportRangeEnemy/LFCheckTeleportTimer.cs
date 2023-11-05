using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckTeleportTimer : Node
    {
        private EnemyTP _enemyTp;
        public LFCheckTeleportTimer(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }

        public override ENodeState CalculateState()
        {
            Debug.Log("LFCheckTeleportTimer");
            return _enemyTp.CanMove ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
