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
            _agent.destination = _agent.transform.position;
            return ENodeState.SUCCESS;
        }
    }
}
