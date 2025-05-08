using Assets._Project.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets._Project.Scripts.Core.Common
{
    public abstract class DamageImpact : CachedMonoBehaviour
    {
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private GameObject particleEffect;

        [SerializeField] private float punchScale = 0.2f;
        [SerializeField] private float punchDuration = 0.2f;
        [SerializeField] private float colorFlashDuration = 0.1f;
        [SerializeField] private float shakeStrength = 0.2f;

        private Material _materialInstance;

        private Vector3 _defaultScale;
        private Color _originalColor;

        protected abstract IHealthHolder _health { get; }

        private void Awake()
        {
            if (meshRenderer != null)
            {
                _materialInstance = meshRenderer.material;
                _originalColor = _materialInstance.color;
            }

            _defaultScale = CachedTrasform.localScale;

            _health.OnHealthChanged += OnHealthChange;
            _health.OnHealthGone += OnHealthGone;
        }
        private void OnHealthChange()
        {
            CachedTrasform.DOKill();
            _materialInstance?.DOKill();

            
            CachedTrasform.DOPunchScale(Vector3.one * punchScale, punchDuration, vibrato: 5, elasticity: 0.5f);

            CachedTrasform.DOShakePosition(punchDuration, shakeStrength, vibrato: 5, randomness: 60f, snapping: false, fadeOut: false);

            if (_materialInstance != null)
            {
                _materialInstance.DOColor(Color.blue, colorFlashDuration)
                    .OnComplete(() =>
                        _materialInstance.DOColor(_originalColor, colorFlashDuration));
            }

            PlayDamageSound();
        }

        protected virtual void PlayDamageSound() { }

        private async void OnHealthGone()
        {
            PlayDeathSound();
            particleEffect.SetActive(true);
            await UniTask.Delay(1000);
            particleEffect.SetActive(false);
        }

        protected virtual void PlayDeathSound() { }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= OnHealthChange;
                _health.OnHealthGone -= OnHealthGone;
            }

            // Destory temporary material
            if (Application.isPlaying && _materialInstance != null)
                Destroy(_materialInstance);
        }
    }
}