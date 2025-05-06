using UnityEngine;

namespace Assets._Project.Scripts.Core.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject tapToStartPanel;

        public void ToggleWinPanel(bool show) => winPanel.SetActive(show);
        public void ToggleLosePanel(bool show) => losePanel.SetActive(show);
        public void ToggleStartPanel(bool show) => tapToStartPanel.SetActive(show);
    }
}