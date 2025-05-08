using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core.UI
{
    public interface ICarShowBar
    {
        public void Show();
        public void Hide();
    }
    public class CarHPBar : CachedMonoBehaviour, ICarShowBar
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private CarAttackTarget carTarget;

        private Transform _cameraTransfrom;
        private int _maxHP;

        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            _cameraTransfrom = Camera.main?.transform;
            _maxHP = carTarget.MaxHealth;

            carTarget.OnHealthChanged += UpdateHP;
            carTarget.OnHealthGone += Hide;

            UpdateHP(carTarget.CurrentHealth);
        }

        private void LateUpdate()
        {
            if (_cameraTransfrom != null)
            {
                CachedTrasform.forward = (_cameraTransfrom.position - CachedTrasform.position).normalized * -1f;
            }
        }

        private void UpdateHP(int current)
        {
            float t = Mathf.Clamp01((float)current / _maxHP);
            fillImage.fillAmount = t;
        }

        public void Show()
        {
            CachedGameObject.SetActive(true);
        }

        public void Hide()
        {
            CachedGameObject.SetActive(false);
        }

        private void OnDisable()
        {
            carTarget.OnHealthChanged -= UpdateHP;
            carTarget.OnHealthGone -= Hide;
        }
    }
}