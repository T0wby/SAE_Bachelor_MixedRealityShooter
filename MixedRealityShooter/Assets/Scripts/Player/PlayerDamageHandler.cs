using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerDamageHandler : MonoBehaviour, IDamage
    {
        [SerializeField] private PlayerStatus _playerStatus;
        public void TakeDamage(int damage)
        {
            _playerStatus.Health -= damage;
        }
    }
}
