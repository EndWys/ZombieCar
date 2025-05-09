using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Core.UI.Panels;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class LoseState : GameState
    {
        private LoseUIPanel _loseUIPanel;
        private ReloadUIPanel _reloadUIPanel;

        private ICarReseter _carReseter;
        public LoseState(LoseUIPanel loseUIPanel, ReloadUIPanel reloadUIPanel,
            ICarReseter carReseter)
        {
            _loseUIPanel = loseUIPanel;
            _reloadUIPanel = reloadUIPanel;

            _carReseter = carReseter;
        }

        public override async void Enter()
        {
            await _loseUIPanel.Show();

            TapInput.OnTap += Restart;
        }

        public override void Exit()
        {
            _carReseter.ResetSelf();
        }

        private async void Restart()
        {
            TapInput.OnTap -= Restart;

            await _reloadUIPanel.Show();
            await _loseUIPanel.Hide();

            _stateSwitcher.SwitchState<WaitForTapState>();
        }
    }
}