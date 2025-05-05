using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.Interfaces;
using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WinState : GameState
    {
        private readonly GameUI _ui;
        private ICarReseter _carReseter;

        public WinState(GameStateMachine machine, GameUI ui, ICarReseter reseter) : base(machine)
        {
            _ui = ui;
            _carReseter = reseter;
        }

        public override void Enter()
        {
            _ui.ToggleWinPanel(true);
            TapInput.OnTap += Restart;
        }

        public override void Exit()
        {
            TapInput.OnTap -= Restart;
        }

        private void Restart()
        {
            _ui.ToggleWinPanel(false);
            _carReseter.ResetSelf();
            _stateMachine.Enter<WaitForTapState>();
        }
    }
}