using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : AWeapon
    {
        [Header("AI Fields")]
        [SerializeField] private Transform _rotationTransform;
        private float _currentAngle = 0.0f;
        private float _rotationStep = 0f;
        private Coroutine _attack;

        public Transform RotationTransform
        {
            get => _rotationTransform;
            set => _rotationTransform = value;
        }
        
        public override void Attack()
        {
            if (_rotationTransform == null) return;
            if (DefaultSettings.SpinningAngle > 0.0f || _attack == null)
            {
                _attack = StartCoroutine(RotateWeapon());
            }
        }

        private IEnumerator RotateWeapon()
        {
            _currentAngle = 0.0f;
            while (_currentAngle < DefaultSettings.SpinningAngle)
            {
                // Calculate the rotation step based on time and speed
                _rotationStep = DefaultSettings.SpinningSpeed * Time.deltaTime;

                // Rotate the object by the rotation step
                _rotationTransform.Rotate(Vector3.up * _rotationStep);

                // Update the current angle
                _currentAngle += _rotationStep;
                yield return null;

            }
            _attack = null;
        }
    }
}
