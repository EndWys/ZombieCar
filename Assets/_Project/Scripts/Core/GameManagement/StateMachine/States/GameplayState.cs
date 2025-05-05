namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameplayState : GameState
    {
        public GameplayState(GameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            // Start moving car
            // Enable Enemies
        }
    }
}