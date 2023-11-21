using System.Collections.Generic;
using Enemies.BehaviorTree;
using Enemies.TeleportRangeEnemy;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.WalkingRangeEnemy
{
    public class BTWalkRange : MyTree
    {
        [SerializeField] private EnemyWR _enemyWr;
        [SerializeField] private EnemyTargetDetection _targetDetection;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new LFCheckHealth(_enemyWr, _enemyWr.Settings.HealthThreshold),
                    new Selector(new List<Node>
                    {
                        // Flee to furthest point
                        new Sequence(new List<Node>
                        {
                            new LFCheckIfFleeing(_enemyWr),
                            new LFFleeToFurthestPoint(_agent, _targetDetection)
                        }),
                        new LFCheckFleeDestination(_enemyWr, _agent),
                        // Heal
                        new Sequence(new List<Node>
                        {
                            new LFCheckForPotion(_enemyWr),
                            new LFHeal(_enemyWr),
                        }),
                    })
                }),
                // Attack Player
                new Sequence(new List<Node>
                {
                    new LFCheckForPlayer(_enemyWr),
                    new LFCheckAttackTimer(_enemyWr),
                    new LFStopMovement(_agent),
                    new LFAttack(_enemyWr),
                }),
                // Stop Movement
                // new Sequence(new List<Node>
                // {
                //     new LFCheckAgentDistance(_agent, _enemyWr, 1.0f),
                //     new LFStopMovement(_agent)
                // }),
                // Walk to player
                new LFSetEnemyDestination(_enemyWr, _agent)
            });
        
            return root;
        }
    }
}
