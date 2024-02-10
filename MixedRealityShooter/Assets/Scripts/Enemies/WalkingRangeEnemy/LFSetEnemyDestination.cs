using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Enemies.WalkingRangeEnemy
{
    public class LFSetEnemyDestination : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly AEnemy _enemy;

        public LFSetEnemyDestination(AEnemy enemy, NavMeshAgent agent)
        {
            _agent = agent;
            _enemy = enemy;
        }
        
        public override ENodeState CalculateState()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_enemy.PlayerTransform.position);
            
            return ENodeState.SUCCESS;
        }
    }
}
