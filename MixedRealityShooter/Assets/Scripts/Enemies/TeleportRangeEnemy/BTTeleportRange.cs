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
                    new LFCheckForPotion(),
                    new LFHeal(_enemy),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckForEnemy(),
                    new LFCheckAttackTimer(),
                    new LFAttack(),
                }),
                new Sequence(new List<Node>
                {
                    new LFCheckTeleportTimer(),
                    new LFTeleport(),
                }),
            });
        
            return root;
        }
    }
}
