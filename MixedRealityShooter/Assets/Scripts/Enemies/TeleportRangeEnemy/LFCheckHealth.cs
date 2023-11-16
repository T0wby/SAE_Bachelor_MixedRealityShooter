using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckHealth : Node
    {
        private int _currHealth;
        private int _healthThreshold;

        public LFCheckHealth(int currHealth, int threshold)
        {
            _currHealth = currHealth;
            _healthThreshold = threshold;
        }

        public override ENodeState CalculateState()
        {
            Debug.LogWarning($"_currHealth: {_currHealth}");
            Debug.LogWarning($"_healthThreshold: {_healthThreshold}");
            return _state = (_currHealth < _healthThreshold) ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }
    }
}
