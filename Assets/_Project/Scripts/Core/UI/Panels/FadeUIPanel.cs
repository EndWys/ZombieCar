using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI.Panels
{
    public class FadeUIPanel : UIPanel
    {
        [SerializeField] private float animationDuration = 0.5f;

        public override async UniTask Show()
        {
            CachedGameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;

            var fade = _canvasGroup.DOFade(1, animationDuration).SetEase(Ease.OutQuad);
            var scale = CachedTrasform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);

            await UniTask.WhenAll(fade.ToUniTask(), scale.ToUniTask());
        }

        public override async UniTask Hide()
        {
            var fade = _canvasGroup.DOFade(0, animationDuration).SetEase(Ease.InQuad);
            var scale = CachedTrasform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack);

            await UniTask.WhenAll(fade.ToUniTask(), scale.ToUniTask());

            CachedGameObject.SetActive(false);
        }
    }
}