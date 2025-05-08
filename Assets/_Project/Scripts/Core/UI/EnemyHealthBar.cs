using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI
{
    public class EnemyHealthBar : HealthBar
    {
        [SerializeField] private EnemyDamageable enemyHealth;
        [SerializeField] private CanvasGroup canvasGroup;
        protected override IHealthHolder _targetHealth => enemyHealth;

        private bool _isShown = false;

        protected override void OnBarEnable()
        {
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

            UpdateHealth();
        }

        public override void Show()
        {
            _isShown = true;
            canvasGroup.alpha = 1f;
        }
    }
}