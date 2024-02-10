using System.Collections.Generic;
using Enemies.BehaviorTree;
using Enemies.TeleportRangeEnemy;
using Enemies.WalkingRangeEnemy;
using UnityEngine;

namespace Enemies.WalkingMeleeEnemy
{
    public class BTWalkMelee : MyTree
    {
        [SerializeField] private EnemyBat _enemyBat;
        [SerializeField] private EnemyTargetDetection _targetDetection;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new LFCheckHealth(_enemyBat, _enemyBat.Settings.HealthThreshold),
                    new Selector(new List<Node>
                    {
                        // Flee to furthest point
                        new Sequence(new List<Node>
                        {
                            new LFCheckIfFleeing(_enemyBat),
                            new LFFleeToFurthestPoint(_agent, _targetDetection)
                        }),
                        new LFCheckFleeDestination(_enemyBat, _agent),
                        // Heal
                        new Sequence(new List<Node>
                        {
                            new LFCheckForPotion(_enemyBat),
                            new LFHeal(_enemyBat),
                        }),
                    })
                }),
                // Attack Player
                new Sequence(new List<Node>
                {
                    new LFCheckForPlayerRange(_enemyBat),
                    new LFCheckAttackTimer(_enemyBat),
                    new LFStopMovement(_agent),
                    new LFAttack(_enemyBat),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckAgentDistance(_agent, _enemyBat, 0.4f),
                    new LFStopMovement(_agent)
                }),
                // Walk to player
                new LFSetEnemyDestination(_enemyBat, _agent)
            });
        
            return root;
        }
    }
}
