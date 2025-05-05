using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class LoseState : GameState
    {
        private readonly GameUI _ui;

        public LoseState(GameStateMachine machine, GameUI ui) : base(machine)
        {
            _ui = ui;
        }

        public override void Enter()
        {
            _ui.ToggleLosePanel(true);
        }

        public override void Exit()
        {
            _ui.ToggleLosePanel(false);
        }
    }
}