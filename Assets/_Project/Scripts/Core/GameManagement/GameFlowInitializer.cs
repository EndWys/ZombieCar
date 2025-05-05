using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.GameManagement.StateMachine;
using Assets._Project.Scripts.Core.GameManagement.StateMachine.States;
using Assets._Project.Scripts.Core.UI;
using VContainer.Unity;

namespace Assets._Project.Scripts.Core.GameManagement
{
    public class GameFlowInitializer : ITickable
    {
        private readonly GameStateMachine _stateMachine;

        public GameFlowInitializer(GameUI ui, RoadFinish finish, CarController car)
        {
            _stateMachine = new GameStateMachine();

            _stateMachine.Register(new WaitForTapState(_stateMachine, ui));
            _stateMachine.Register(new GameRunState(_stateMachine, car, finish));
            _stateMachine.Register(new WinState(_stateMachine, ui, car));
            _stateMachine.Register(new LoseState(_stateMachine, ui));

            _stateMachine.Enter<WaitForTapState>();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}