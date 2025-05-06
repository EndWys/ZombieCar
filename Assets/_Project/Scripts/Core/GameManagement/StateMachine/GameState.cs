using Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates;
using Assets._Project.Scripts.Core.GameManagement.StateMachine.States;
using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine
{
    public abstract class GameState : IState
    {
        protected IStateSwitcher<GameState> _stateSwitcher;
        protected GameStateContext _stateContext;

        public GameState(IStateSwitcher<GameState> stateSwitcher, GameStateContext context)
        {
            _stateSwitcher = stateSwitcher;
            _stateContext = context;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Tick() { }
    }
}