using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Player
{
    public class PlayerStatus : MonoBehaviour, IDamage
    {
        [SerializeField] private int _health = 100;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnHealthChange.Invoke(_health);
            }
        }

        public UnityEvent<int> OnHealthChange;
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
