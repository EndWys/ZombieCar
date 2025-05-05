namespace Assets._Project.Scripts.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}