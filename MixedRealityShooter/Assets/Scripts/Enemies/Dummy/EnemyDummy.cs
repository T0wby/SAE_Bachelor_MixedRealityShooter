using TMPro;
using UnityEngine;

namespace Enemies.Dummy
{
    public class EnemyDummy : AEnemy
    {
        [SerializeField] private TMP_Text _damageText;
        
        public override void TakeDamage(int damage)
        {
            CurrHealth -= damage;
            if (CurrHealth == 0)
                CurrHealth = 100;
            _damageText.text = damage.ToString();
        }
    }
}
