using System.Collections.Generic;
using Enemys.BehaviorTree;

namespace Enemys.TeleportRangeEnemy
{
    public class BTTeleportRange : MyTree
    {
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
            
            });
        
            return root;
        }
    }
}
