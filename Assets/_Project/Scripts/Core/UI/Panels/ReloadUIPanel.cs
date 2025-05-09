using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core.UI.Panels
{
    public class ReloadUIPanel : UIPanel
    {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private Image circleImage;

        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private double panelHideDelay = 0.1f;

        private RectTransform _circleRect;

        private void Awake()
        {
            _circleRect = circleImage.rectTransform;
        }

        public override async UniTask Show()
        {
            if (CachedGameObject.activeInHierarchy)
            {
                Debug.Log($"{typeof(ReloadUIPanel).Name} in {CachedGameObject.name} is already shown");
                return;
            }

            gameObject.SetActive(true);
            _circleRect.sizeDelta = Vector2.zero;

            float maxRadius = GetMaxScreenRadius();

            await _circleRect
                .DOSizeDelta(Vector2.one * maxRadius * 2f, animationDuration)
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
            if (!CachedGameObject.activeInHierarchy){
                Debug.Log($"{typeof(ReloadUIPanel).Name} in {CachedGameObject.name} is already hiden");
                return;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(panelHideDelay));

            await _circleRect
                .DOSizeDelta(Vector2.zero, animationDuration)
                .SetEase(Ease.InCubic)
                .ToUniTask();


            CachedGameObject.SetActive(false);
        }
    }
}