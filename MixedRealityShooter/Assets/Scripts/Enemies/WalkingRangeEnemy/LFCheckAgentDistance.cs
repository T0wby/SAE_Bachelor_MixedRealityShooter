using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFCheckAgentDistance : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _playerPos;
        private readonly float _distanceToStop;
        
        public LFCheckAgentDistance(NavMeshAgent agent, AEnemy enemyWr, float distanceToStop)
        {
            _agent = agent;
            _playerPos = enemyWr.PlayerTransform.position;
            _distanceToStop = distanceToStop;
        }
        
        public override ENodeState CalculateState()
        {
            return CheckIfMinValueReached() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }

        private bool CheckIfMinValueReached()
        {
            return (_agent.transform.position - _playerPos).magnitude < _distanceToStop;
        }
    }
}
