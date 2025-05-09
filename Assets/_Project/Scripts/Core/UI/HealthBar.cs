using Assets._Project.Scripts.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core.UI
{
    public abstract class HealthBar : CachedMonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private Image delayedFillImage;

        [SerializeField] private float delayBeforeReduce = 0.5f;
        [SerializeField] private float reduceSpeed = 1f;

        private Transform _cameraTransfrom;
        private int _maxHealth;

        private Coroutine _delayedFillRoutine;

        protected abstract IHealthHolder _targetHealth { get; }

        private void OnEnable()
        {
            OnHealthBarEnable();
        }

        protected virtual void OnHealthBarEnable()
        {
            _cameraTransfrom = Camera.main?.transform;
            _maxHealth = _targetHealth.MaxHealth;

            UpdateHealthInstant();

            _targetHealth.OnHealthChanged += OnHealthChanged;
            _targetHealth.OnHealthGone += OnHealthGone;
        }

        protected void UpdateHealthInstant()
        {
            float t = Mathf.Clamp01((float)_targetHealth.CurrentHealth / _maxHealth);
            fillImage.fillAmount = t;
            delayedFillImage.fillAmount = t;
        }

        protected void UpdateHealthSmooth()
        {
            float t = Mathf.Clamp01((float)_targetHealth.CurrentHealth / _maxHealth);
            fillImage.fillAmount = t;

            if (_delayedFillRoutine != null)
                StopCoroutine(_delayedFillRoutine);
            _delayedFillRoutine = StartCoroutine(AnimateDelayedBar(t));
        }

        private IEnumerator AnimateDelayedBar(float target)
        {
            yield return new WaitForSeconds(delayBeforeReduce);

            while (delayedFillImage.fillAmount > target)
            {
                delayedFillImage.fillAmount = Mathf.MoveTowards(
                    delayedFillImage.fillAmount, target, reduceSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public abstract void OnHealthChanged();
        public abstract void OnHealthGone();
        public abstract void Show();
        public abstract void Hide();

        private void LateUpdate()
        {
            LookToCamera();
        }

        private void LookToCamera()
        {
            if (_cameraTransfrom != null)
            {
                CachedTrasform.forward = (_cameraTransfrom.position - CachedTrasform.position).normalized * -1f;
            }
        }

        private void OnDisable()
        {
            _targetHealth.OnHealthChanged -= OnHealthChanged;
            _targetHealth.OnHealthGone -= OnHealthGone;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}