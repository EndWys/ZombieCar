using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core.UI.Panels
{
    public class ReloadUIPanel : UIPanel
    {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private Image circleImage;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private int panelHideDelayMs = 200;

        private RectTransform _circleRect;

        private void Awake()
        {
            _circleRect = circleImage.rectTransform;

            _circleRect.sizeDelta = Vector2.zero;
        }

        public override async UniTask Show()
        {
            gameObject.SetActive(true);
            _circleRect.sizeDelta = Vector2.zero;

            float maxRadius = GetMaxScreenRadius();

            await _circleRect
                .DOSizeDelta(Vector2.one * maxRadius * 2f, duration)
                .SetEase(Ease.OutCubic)
                .ToUniTask();
        }
        private float GetMaxScreenRadius()
        {
            float width = _canvas.rect.width;
            float height = _canvas.rect.height;
            return Mathf.Sqrt(width * width + height * height) * 0.5f;
        }

        public override async UniTask Hide()
        {
            await _circleRect
                .DOSizeDelta(Vector2.zero, duration)
                .SetEase(Ease.InCubic)
                .ToUniTask();

            await UniTask.Delay(panelHideDelayMs);

            gameObject.SetActive(false);
        }
    }
}