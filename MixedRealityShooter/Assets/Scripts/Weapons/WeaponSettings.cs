using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "NewWeaponSettings", menuName = "Weapons/Settings", order = 0)]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private int damage;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float bulletsPerSecond;

        public string WeaponName => weaponName;
        public int Damage => damage;
        public float ProjectileSpeed => projectileSpeed;
        public float BulletsPerSecond => bulletsPerSecond;
    }
}