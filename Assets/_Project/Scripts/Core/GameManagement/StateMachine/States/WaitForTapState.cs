using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WaitForTapState : GameState
    {
        private readonly GameUI _ui;

        public WaitForTapState(GameStateMachine machine, GameUI ui) : base(machine)
        {
            _ui = ui;
        }

        public override void Enter()
        {
            _ui.ToggleStartPanel(true);
            TapInput.OnTap += OnTap;
        }

        public override void Exit()
        {
            TapInput.OnTap -= OnTap;
            _ui.ToggleStartPanel(false);
        }

        private void OnTap()
        {
            _stateMachine.Enter<GameplayState>();
        }
    }
}