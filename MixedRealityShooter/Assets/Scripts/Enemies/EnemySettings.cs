using UnityEngine;
using Utility;

namespace Enemies
{
    [CreateAssetMenu(fileName = "NewEnemySettings", menuName = "Enemies/Settings", order = 0)]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private EEnemyType _type = EEnemyType.Range;
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _moneyValue;
        [SerializeField] private int _healthPotionAmount;
        [SerializeField] private int _healthThreshold;
        [SerializeField] private float _attackTimer;
        [SerializeField] private float _moveTimer;
        [SerializeField] private float _searchRange;
        [SerializeField] private float _fov;
        [SerializeField] private GameObject _weapon;

        public string WeaponName => _name;
        public EEnemyType EnemyType => _type;
        public int Health => _health;
        public int Damage => _damage;
        public int MoneyValue => _moneyValue;
        public int HealthPotionAmount => _healthPotionAmount;
        public int HealthThreshold => _healthThreshold;
        public float AttackTimer => _attackTimer;
        public float MoveTimer => _moveTimer;
        public float SearchRange => _searchRange;
        public float FOV => _fov;
        public GameObject Weapon => _weapon;
    }
}
