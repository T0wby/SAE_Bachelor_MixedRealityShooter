using System.Collections.Generic;
using Enemies.BehaviorTree;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies.TeleportRangeEnemy
{
    public class BTTeleportRange : MyTree
    {
        [FormerlySerializedAs("_enemy")] [SerializeField] private EnemyTP enemyTp;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new LFCheckHealth(enemyTp.CurrHealth, enemyTp.Settings.HealthThreshold),
                    new LFCheckForPotion(enemyTp),
                    new LFHeal(enemyTp),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckForEnemy(enemyTp),
                    new LFCheckAttackTimer(enemyTp),
                    new LFAttack(enemyTp),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckTeleportTimer(enemyTp),
                    new LFTeleport(enemyTp),
                }),
            });
        
            return root;
        }
    }
}
