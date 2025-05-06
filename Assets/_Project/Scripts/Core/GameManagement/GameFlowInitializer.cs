using Assets._Project.Scripts.Core.GameManagement.StateMachine;
using Assets._Project.Scripts.Core.GameManagement.StateMachine.States;
using VContainer.Unity;

namespace Assets._Project.Scripts.Core.GameManagement
{
    public class GameFlowInitializer : ITickable
    {
        private GameStateMachine _stateMachine;

        public GameFlowInitializer(WaitForTapState waitForTapState, GameRunState gameRunState, WinState winState, LoseState loseState)
        {
            _stateMachine = new GameStateMachine();

            _stateMachine.Register(waitForTapState);
            _stateMachine.Register(gameRunState);
            _stateMachine.Register(winState);
            _stateMachine.Register(loseState);

            _stateMachine.SwitchState<WaitForTapState>();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}