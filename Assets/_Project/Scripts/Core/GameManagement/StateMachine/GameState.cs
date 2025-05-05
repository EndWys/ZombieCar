using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine
{
    public abstract class GameState : IState
    {
        protected GameStateMachine _stateMachine;

        public GameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Tick() { }
    }
}