using Enemies.BehaviorTree;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFStopMovement : Node
    {
        private readonly NavMeshAgent _agent;
        
        public LFStopMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }
        
        public override ENodeState CalculateState()
        {
            return StopMovingToDestination() ? ENodeState.SUCCESS : ENodeState.FAILURE;
        }

        private bool StopMovingToDestination()
        {
            _agent.isStopped = true;
            return _agent.isStopped;
        }
    }
}
