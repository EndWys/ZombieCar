using Assets._Project.Scripts.Core.PlayerLogic.Car;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI
{
    public interface ICarShowBar
    {
        public void Show();
        public void Hide();
    }
    public class CarHealthBar : HealthBar, ICarShowBar
    {
        [SerializeField] private CarAttackTarget carTarget;
        [SerializeField] private CanvasGroup canvasGroup;

        protected override IHealthHolder _targetHealth => carTarget;

        private void Awake()
        {
            Hide();
        }

        public override void OnHealthGone()
        {
            Hide();
        }
        public override void Hide()
        {
            CachedGameObject.SetActive(false);
        }


        public override void OnHealthChanged()
        {
            UpdateHealthSmooth();
        }

        public override void Show()
        {
            CachedGameObject.SetActive(true);
        }
    }
}