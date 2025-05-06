using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WaitForTapState : GameState
    {
        public WaitForTapState(IStateSwitcher<GameState> stateMachine, GameStateContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            _stateContext.CarHealth.ResetHealth();
            _stateContext.EnemySpawner.DeactivateAll();
            _stateContext.UI.ToggleStartPanel(true);
            TapInput.OnTap += OnTap;
        }

        public override void Exit()
        {
            TapInput.OnTap -= OnTap;
        }

        private void OnTap()
        {
            _stateContext.EnemySpawner.Spawn();
            _stateContext.UI.ToggleStartPanel(false);
            _stateSwitcher.SwitchState<GameRunState>();
        }
    }
}