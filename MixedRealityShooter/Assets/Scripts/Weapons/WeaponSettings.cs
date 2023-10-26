using UnityEngine;
using Utility;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "Weapons/Settings", order = 0)]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private string _weaponName;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private GameObject _weaponProp;
        [SerializeField] private EWeaponType _type = EWeaponType.AssaultRifle;
        [SerializeField] private int _damage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _bulletsPerSecond;

        public string WeaponName => _weaponName;
        public GameObject WeaponPrefab => _weaponPrefab;
        public GameObject WeaponProp => _weaponProp;
        public EWeaponType WeaponType => _type;
        public int Damage => _damage;
        public float ProjectileSpeed => _projectileSpeed;
        public float BulletsPerSecond => _bulletsPerSecond;
    }
}