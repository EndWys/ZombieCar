namespace Assets._Project.Scripts.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Tick();
    }
}