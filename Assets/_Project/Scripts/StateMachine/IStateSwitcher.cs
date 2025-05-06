namespace Assets._Project.Scripts.StateMachine
{
    public interface IStateSwitcher<TState> where TState : IState
    {
        public void SwitchState<T>() where T : TState;
    }
}