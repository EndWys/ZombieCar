using Assets._Project.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : CachedMonoBehaviour
    {
        protected CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public abstract UniTask Hide();

        public abstract UniTask Show();
    }
}