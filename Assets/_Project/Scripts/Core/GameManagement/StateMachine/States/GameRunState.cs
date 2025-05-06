using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameRunState : GameState
    {
        public GameRunState(IStateSwitcher<GameState> stateMachine, GameStateContext context) : base(stateMachine, context)
        {
        }

        public override void Enter()
        {
            _stateContext.CarEngine.StartMoving();

            _stateContext.CarHealth.OnHealthGone += OnDeath;
            _stateContext.RoadFinish.OnFinishReached += OnFinish;
        }

        public override void Exit()
        {
            _stateContext.CarHealth.OnHealthGone -= OnDeath;
            _stateContext.RoadFinish.OnFinishReached -= OnFinish;
        }

        private void OnFinish()
        {
            _stateContext.CarEngine.StopMoving();
            _stateSwitcher.SwitchState<WinState>();
        }

        private void OnDeath()
        {
            _stateContext.CarEngine.StopMoving();
            _stateSwitcher.SwitchState<LoseState>();
        }
    }
}