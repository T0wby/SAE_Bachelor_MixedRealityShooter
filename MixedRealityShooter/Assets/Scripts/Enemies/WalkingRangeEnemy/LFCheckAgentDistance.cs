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
        private Vector3 _playerPos;
        private Vector2 _playerPoint;
        private Vector3 _agentPos;
        private Vector2 _agentPoint;
        
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
            _playerPos = _enemy.PlayerTransform.position;
            _agentPos = _agent.transform.position;
            _agentPoint = new Vector2(_agentPos.x, _agentPos.z);
            _playerPoint = new Vector2(_playerPos.x, _playerPos.z);
            //Debug.LogWarning((_agentPoint - _playerPoint).magnitude);
            return (_agentPoint - _playerPoint).magnitude < _distanceToStop;
        }
    }
}
