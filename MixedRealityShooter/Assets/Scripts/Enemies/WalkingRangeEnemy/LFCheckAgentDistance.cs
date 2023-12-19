using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFCheckAgentDistance : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly AEnemy _enemy;
        private readonly float _distanceToStop;
        
        public LFCheckAgentDistance(NavMeshAgent agent, AEnemy enemyWr, float distanceToStop)
        {
            _agent = agent;
            _enemy = enemyWr;
            _distanceToStop = distanceToStop;
        }
        
        public override ENodeState CalculateState()
        {
            return CheckIfMinValueReached() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }

        private bool CheckIfMinValueReached()
        {
            return (_agent.transform.position - _enemy.PlayerTransform.position).magnitude < _distanceToStop;
        }
    }
}
