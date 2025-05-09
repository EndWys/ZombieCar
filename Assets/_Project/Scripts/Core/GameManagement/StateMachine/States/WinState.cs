using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Core.UI.Panels;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WinState : GameState
    {
        private WinUIPanel _winUIPanel;
        private ReloadUIPanel _reloadUIPanel;

        private ICarReseter _carReseter;

        public WinState(WinUIPanel winUIPanel, ReloadUIPanel reloadUIPanel,
            ICarReseter carReseter)
        {
            _winUIPanel = winUIPanel;
            _reloadUIPanel = reloadUIPanel;

            _carReseter = carReseter;
        }

        public override async void Enter()
        {
            await _winUIPanel.Show();
            TapInput.OnTap += Restart;
        }

        public override void Exit()
        {
            TapInput.OnTap -= Restart;
        }

        private async void Restart()
        {
            await _reloadUIPanel.Show();
            await _winUIPanel.Hide();
            _carReseter.ResetSelf();
            _stateSwitcher.SwitchState<WaitForTapState>();
        }
    }
}