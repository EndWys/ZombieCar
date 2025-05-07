using Assets._Project.Scripts.Core.UI.Panels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Core.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private ReloadUIPanel reloadUIPanel;
        [SerializeField] private FadeUIPanel winPanel;
        [SerializeField] private FadeUIPanel losePanel;
        [SerializeField] private FadeUIPanel tapToStartPanel;

        public async UniTask ToggleReloadPanel(bool show)
        {
            if (show)
                await reloadUIPanel.Show();
            else
                await reloadUIPanel.Hide();
        }

        public async UniTask ToggleWinPanel(bool show)
        {
            if (show)
                await winPanel.Show();
            else
                await winPanel.Hide();
        }

        public async UniTask ToggleLosePanel(bool show)
        {
            if (show)
                await losePanel.Show();
            else
                await losePanel.Hide();
        }

        public async UniTask ToggleStartPanel(bool show)
        {
            if(show)
                await tapToStartPanel.Show();
            else
                await tapToStartPanel.Hide();
        }
    }
}