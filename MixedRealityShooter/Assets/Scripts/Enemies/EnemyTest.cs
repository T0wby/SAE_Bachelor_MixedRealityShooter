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
            var tmp = other.transform.parent.GetComponent<PlayerStatus>();
            if (tmp != null)
            {
                Debug.Log($"{gameObject.name} dealt {_damage} damage!");
                tmp.TakeDamage(_damage);
            }
        }
    }
}
