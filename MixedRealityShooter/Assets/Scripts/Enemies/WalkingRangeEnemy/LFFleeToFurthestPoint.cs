using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFFleeToFurthestPoint : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly EnemyTargetDetection _targetDetection;
        
        public LFFleeToFurthestPoint(NavMeshAgent agent, EnemyTargetDetection targetDetection)
        {
            _agent = agent;
            _targetDetection = targetDetection;
        }

        public override ENodeState CalculateState()
        {
            if (NavMesh.SamplePosition(_targetDetection.GetSpawnPointTransform(false).position, out NavMeshHit myNavHit, 100 , -1))
            {
                _agent.SetDestination(myNavHit.position);
                return ENodeState.SUCCESS;
            }
            //_agent.SetDestination(_targetDetection.GetSpawnPointTransform(false).position);
            Debug.LogWarning("FleeFailure");
            return ENodeState.FAILURE;
        }
    }
}
