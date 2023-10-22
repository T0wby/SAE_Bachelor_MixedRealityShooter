using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckForEnemy : Node
    {
        private Enemy _enemy;
        
        public LFCheckForEnemy(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override ENodeState CalculateState()
        {
            Debug.Log("LFCheckForEnemy");
            return _enemy.CheckForPlayerInSight() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
