using System.Collections.Generic;
using Enemies.BehaviorTree;
using Enemies.TeleportRangeEnemy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemies.WalkingRangeEnemy
{
    public class BTWalkRange : MyTree
    {
        [FormerlySerializedAs("_enemyWr")] [SerializeField] private EnemyWalkRange enemyWalkRange;
        [SerializeField] private EnemyTargetDetection _targetDetection;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new LFCheckHealth(enemyWalkRange, enemyWalkRange.Settings.HealthThreshold),
                    new Selector(new List<Node>
                    {
                        // Flee to furthest point
                        new Sequence(new List<Node>
                        {
                            new LFCheckIfFleeing(enemyWalkRange),
                            new LFFleeToFurthestPoint(_agent, _targetDetection)
                        }),
                        new LFCheckFleeDestination(enemyWalkRange, _agent),
                        // Heal
                        new Sequence(new List<Node>
                        {
                            new LFCheckForPotion(enemyWalkRange),
                            new LFHeal(enemyWalkRange),
                        }),
                    })
                }),
                // Attack Player
                new Sequence(new List<Node>
                {
                    new LFCheckForPlayer(enemyWalkRange),
                    new LFCheckAttackTimer(enemyWalkRange),
                    new LFStopMovement(_agent),
                    new LFAttack(enemyWalkRange),
                }),
                // Stop Movement
                 new Sequence(new List<Node>
                 {
                     new LFCheckAgentDistance(_agent, enemyWalkRange, 1.0f),
                     new LFStopMovement(_agent)
                 }),
                // Walk to player
                new LFSetEnemyDestination(enemyWalkRange, _agent)
            });
        
            return root;
        }
    }
}
