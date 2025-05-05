using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WinState : GameState
    {
        private readonly GameUI _ui;

        public WinState(GameStateMachine machine, GameUI ui) : base(machine)
        {
            _ui = ui;
        }

        public override void Enter()
        {
            _ui.ToggleWinPanel(true);
        }

        public override void Exit()
        {
            _ui.ToggleWinPanel(false);
        }
    }
}