using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFAttack : Node
    {
        public LFAttack()
        {
            
        }
        
        public override ENodeState CalculateState()
        {
            var tmp = GetData("target");

            if (tmp == null) return ENodeState.FAILURE;
            
            return _state;
        }
    }
}
