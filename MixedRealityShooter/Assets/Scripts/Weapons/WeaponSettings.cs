using UnityEngine;
using Utility;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "Weapons/Settings", order = 0)]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private string _weaponName;
        [SerializeField] private EWeaponType _type = EWeaponType.AssaultRifle;
        [SerializeField] private int _damage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _bulletsPerSecond;

        public string WeaponName => _weaponName;
        public EWeaponType WeaponType => _type;
        public int Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
        public float BulletsPerSecond => _bulletsPerSecond;
    }
}