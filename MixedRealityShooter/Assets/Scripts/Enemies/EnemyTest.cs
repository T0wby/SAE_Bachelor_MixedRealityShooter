using System;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyTest : MonoBehaviour
    {
        private int _damage = 10;

        private void OnCollisionEnter(Collision other)
        {
            Transform tmpTR = other.transform.parent;
            if (tmpTR == null) return;
            var tmp = tmpTR.GetComponent<PlayerStatus>();
            if (tmp != null)
            {
                Debug.Log($"{gameObject.name} dealt {_damage} damage!");
                tmp.TakeDamage(_damage);
            }
        }
    }
}
