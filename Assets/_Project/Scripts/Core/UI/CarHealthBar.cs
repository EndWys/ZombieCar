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
            canvasGroup.alpha = 0f;
        }


        public override void OnHealthChanged()
        {
            UpdateHealth();
        }

        public override void Show()
        {
            canvasGroup.alpha = 1f;
        }
    }
}