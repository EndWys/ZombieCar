using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public abstract class EnemyState : State<EnemyState>
    {
        protected EnemyStateContext _stateContext;

        public EnemyState(EnemyStateContext stateContext)
        {
            _stateContext = stateContext;
        }

        public override void Enter() { }
        public override void Exit() { }
        public override void Tick() { }
    }
}