using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class LoseState : GameState
    {
        private GameUI _gameUI;
        private ICarReseter _carReseter;
        public LoseState(GameUI gameUI, ICarReseter carReseter)
        {
            _gameUI = gameUI;
            _carReseter = carReseter;
        }

        public override void Enter()
        {
            _gameUI.ToggleLosePanel(true);
            TapInput.OnTap += Restart;
        }

        public override void Exit()
        {
            TapInput.OnTap -= Restart;
        }

        private void Restart()
        {
            _gameUI.ToggleLosePanel(false);
            _carReseter.ResetSelf();
            _stateSwitcher.SwitchState<WaitForTapState>();
        }
    }
}