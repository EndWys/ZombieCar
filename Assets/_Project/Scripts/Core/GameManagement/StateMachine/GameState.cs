using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine
{
    public abstract class GameState : State<GameState>
    {
        public override void Enter() { }
        public override void Exit() { }
        public override void Tick() { }
    }
}