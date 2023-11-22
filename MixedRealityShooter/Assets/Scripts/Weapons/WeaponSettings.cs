using UnityEngine;
using Utility;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "Weapons/Settings", order = 0)]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private string _weaponName;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private EWeaponType _type = EWeaponType.AssaultRifle;
        [SerializeField] private int _damage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _bulletsPerSecond;
        [SerializeField] private int _value;

        public string WeaponName => _weaponName;
        public GameObject WeaponPrefab => _weaponPrefab;
        public EWeaponType WeaponType => _type;
        public int Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
        public float BulletsPerSecond => _bulletsPerSecond;
        public int Value => _value;
    }
}