using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.GameManagement.StateMachine;
using Assets._Project.Scripts.Core.GameManagement.StateMachine.States;
using Assets._Project.Scripts.Core.PlayerLogic;
using Assets._Project.Scripts.Core.UI;
using VContainer.Unity;

namespace Assets._Project.Scripts.Core.GameManagement
{
    public class GameFlowInitializer : ITickable
    {
        private GameStateMachine _stateMachine;

        public GameFlowInitializer(GameUI ui, RoadFinish finish, EnemySpawner enemySpawner, CarController car, CarAttackTarget carAttackTarget)
        {
            GameStateContext context = new GameStateContext(ui, car, car, carAttackTarget, finish, enemySpawner);

            _stateMachine = new GameStateMachine();

            _stateMachine.Register(new WaitForTapState(_stateMachine, context));
            _stateMachine.Register(new GameRunState(_stateMachine, context));
            _stateMachine.Register(new WinState(_stateMachine, context));
            _stateMachine.Register(new LoseState(_stateMachine, context));

            _stateMachine.SwitchState<WaitForTapState>();
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}