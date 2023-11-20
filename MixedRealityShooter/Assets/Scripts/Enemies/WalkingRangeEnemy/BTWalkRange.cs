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
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                // new Sequence(new List<Node>
                // {
                //     //new LFCheckHealth(enemyTp.CurrHealth, enemyTp.Settings.HealthThreshold),
                //     //new LFCheckForPotion(enemyTp),
                //     //new LFHeal(enemyTp),
                // }),
                new Sequence(new List<Node>
                {
                    new LFCheckForPlayer(_enemyWr),
                    new LFCheckAttackTimer(_enemyWr),
                    new LFStopMovement(_agent),
                    new LFAttack(_enemyWr),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckAgentDistance(_agent, _enemyWr, 1.0f),
                    new LFStopMovement(_agent)
                }),
                new LFSetEnemyDestination(_enemyWr, _agent)
            });
        
            return root;
        }
    }
}
