using Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public class EnemyAI
    {
        private EnemyStateMachine _stateMachine;

        public void Init(EnemyStateContext context)
        {
            _stateMachine = new EnemyStateMachine();

            _stateMachine.Register(new EnemyIdleState(context));
            _stateMachine.Register(new EnemyWanderState(context));
            _stateMachine.Register(new EnemyChaseState(context));
            _stateMachine.Register(new EnemyDeadState(context));
            _stateMachine.Register(new EnemyDeactivatedState(context));
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }

        public void Activate()
        {
            _stateMachine.SwitchState<EnemyIdleState>();
        }

        public void Deactivate()
        {
            _stateMachine.SwitchState<EnemyDeactivatedState>();
        }
    }
}