using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerDamageHandler : MonoBehaviour, IDamage
    {
        [SerializeField] private PlayerStatus _playerStatus;
        [Header("DamageEffect")]
        [SerializeField] private OVRPassthroughLayer _layer;
        [SerializeField] private Color _startColor = new Color(1, 0, 0, 0);
        [SerializeField] private Color _targetColor = new Color(1, 0, 0, 1);
        private Color _currColor;
        private float _lerpDuration = 0.5f;
        private float _lerpPauseDuration = 0.5f;
        private Coroutine _runningEffect;
        
        
        private void Awake()
        {
            _layer = FindObjectOfType<OVRPassthroughLayer>();
        }

        public void TakeDamage(int damage)
        {
            _playerStatus.Health -= damage;
            if (_runningEffect != null)return;
            _runningEffect = StartCoroutine(LerpColorAlpha());
        }
        
        private IEnumerator LerpColorAlpha()
        {
            float elapsedTime = 0.0f;

            while (elapsedTime < _lerpDuration)
            {
                float t = elapsedTime / _lerpDuration;
                _currColor = Color.Lerp(_startColor, _targetColor, t);

                _layer.edgeColor = _currColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            // Ensure the color reaches its target
            _layer.edgeColor = _targetColor;
            elapsedTime = 0.0f;
            yield return new WaitForSeconds(_lerpPauseDuration);

            while (elapsedTime < _lerpDuration)
            {
                float t = elapsedTime / _lerpDuration;
                _currColor = Color.Lerp(_targetColor, _startColor, t);

                _layer.edgeColor = _currColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            // Ensure the color reaches its target
            _layer.edgeColor = _startColor;
            _runningEffect = null;
        }
    }
}
