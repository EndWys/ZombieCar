using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI.HealthBars
{
    public class EnemyHealthBar : HealthBar
    {
        [SerializeField] private EnemyDamageable enemyHealth;
        [SerializeField] private CanvasGroup canvasGroup;
        protected override IHealthHolder _targetHealth => enemyHealth;

        private bool _isShown = false;

        protected override void OnHealthBarEnable()
        {
            base.OnHealthBarEnable();
            Hide();
        }

        public override void OnHealthGone()
        {
            Hide();
        }

        public override void Hide()
        {
            _isShown = false;
            canvasGroup.alpha = 0f;
        }

        public override void OnHealthChanged()
        {
            if (!_isShown)
                Show();

            UpdateHealthSmooth();
        }

        public override void Show()
        {
            _isShown = true;
            canvasGroup.alpha = 1f;
        }
    }
}