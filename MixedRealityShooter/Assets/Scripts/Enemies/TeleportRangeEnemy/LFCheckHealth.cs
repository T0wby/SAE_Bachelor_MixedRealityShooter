using Enemies.BehaviorTree;
using UnityEngine;
using Utility;

namespace Enemies.TeleportRangeEnemy
{
    public class LFCheckHealth : Node
    {
        private AEnemy _enemy;
        private int _healthThreshold;

        public LFCheckHealth(AEnemy enemy, int threshold)
        {
            _enemy = enemy;
            _healthThreshold = threshold;
        }

        public override ENodeState CalculateState()
        {
            if (_enemy.CurrHealth < _healthThreshold)
                return ENodeState.SUCCESS;
            _enemy.IsFleeing = false;
            return ENodeState.FAILURE;
        }
    }
}
