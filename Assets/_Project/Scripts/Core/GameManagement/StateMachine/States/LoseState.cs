using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class LoseState : GameState
    {

        public LoseState(IStateSwitcher<GameState> stateMachine, GameStateContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            _stateContext.UI.ToggleLosePanel(true);
            TapInput.OnTap += Restart;
        }

        public override void Exit()
        {
            TapInput.OnTap -= Restart;
        }

        private void Restart()
        {
            _stateContext.UI.ToggleLosePanel(false);
            _stateContext.CarReseter.ResetSelf();
            _stateSwitcher.SwitchState<WaitForTapState>();
        }
    }
}