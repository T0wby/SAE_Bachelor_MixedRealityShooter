using System.Collections.Generic;
using Enemies.BehaviorTree;
using UnityEngine;

namespace Enemies.TeleportRangeEnemy
{
    public class BTTeleportRange : MyTree
    {
        [SerializeField] private Enemy _enemy;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new LFCheckHealth(_enemy.CurrHealth, _enemy.Settings.HealthThreshold),
                    new LFCheckForPotion(_enemy),
                    new LFHeal(_enemy),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckForEnemy(_enemy),
                    new LFCheckAttackTimer(_enemy),
                    new LFAttack(_enemy),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckTeleportTimer(_enemy),
                    new LFTeleport(_enemy),
                }),
            });
        
            return root;
        }
    }
}
