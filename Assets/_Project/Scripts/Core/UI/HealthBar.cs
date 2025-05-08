using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core.UI
{
    public abstract class HealthBar : CachedMonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private Transform _cameraTransfrom;

        private int _maxHP;

        protected abstract IHealthHolder _targetHealth { get; }

        private void OnEnable()
        {
            _cameraTransfrom = Camera.main?.transform;
            _maxHP = _targetHealth.MaxHealth;

            UpdateHealth();

            _targetHealth.OnHealthChanged += OnHealthChanged;
            _targetHealth.OnHealthGone += OnHealthGone;

            OnBarEnable();
        }

        protected abstract void OnBarEnable();

        protected void UpdateHealth()
        {
            float t = Mathf.Clamp01((float)_targetHealth.CurrentHealth / _maxHP);
            fillImage.fillAmount = t;
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
    }
}