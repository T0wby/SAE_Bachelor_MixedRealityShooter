using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFTeleport : Node
    {
        private EnemyTP _enemyTp;
        public LFTeleport(EnemyTP enemyTp)
        {
            _enemyTp = enemyTp;
        }

        public override ENodeState CalculateState()
        {
            Debug.Log("LFTeleport");
            _enemyTp.StartTeleport();
            return ENodeState.SUCCESS;
        }
    }
}
