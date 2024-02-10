using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFCheckFleeDestination : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly AEnemy _enemy;
        
        public LFCheckFleeDestination( AEnemy enemy, NavMeshAgent agent)
        {
            _enemy = enemy;
            _agent = agent;
        }

        public override ENodeState CalculateState()
        {
            if (_enemy.IsFleeing && _agent.remainingDistance > 0.5f)
            {
                return ENodeState.SUCCESS;
            }
            return ENodeState.FAILURE;
        }
    }
}
