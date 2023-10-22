using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFHeal : Node
    {
        private Enemy _enemySelf;
        
        public LFHeal(Enemy enemySelf)
        {
            _enemySelf = enemySelf;
        }
        
        public override ENodeState CalculateState()
        {
            Debug.Log("LFHeal");
            _enemySelf.Heal();
            return ENodeState.SUCCESS;
        }
    }
}
