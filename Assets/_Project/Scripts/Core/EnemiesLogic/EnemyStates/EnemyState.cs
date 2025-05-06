using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public abstract class EnemyState : IState
    {
        protected IStateSwitcher<EnemyState> _stateSwitcher;
        protected EnemyStateContext _stateContext;

        public EnemyState(IStateSwitcher<EnemyState> stateSwitcher, EnemyStateContext stateContext)
        {
            _stateSwitcher = stateSwitcher;
            _stateContext = stateContext;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Tick() { }
    }
}