using UnityEngine;

namespace Enemies.Dummy
{
    public class EnemyDummy : AEnemy
    {
        public override void TakeDamage(int damage)
        {
            CurrHealth -= damage;
            if (CurrHealth == 0)
                CurrHealth = 100;
        }
    }
}
