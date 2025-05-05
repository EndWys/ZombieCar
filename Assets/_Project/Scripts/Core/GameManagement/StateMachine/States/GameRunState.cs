using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameRunState : GameState
    {
        private CarController _car;
        private RoadFinish _finish;

        public GameRunState(GameStateMachine stateMachine, CarController car, RoadFinish finish) : base(stateMachine)
        {
            _car = car;
            _finish = finish;
        }

        public override void Enter()
        {
            _car.StartMoving();

            _finish.OnFinishReached += OnFinish;
        }

        public override void Exit()
        {
            _finish.OnFinishReached -= OnFinish;
        }

        private void OnFinish()
        {
            _car.StopMoving();
            _stateMachine.Enter<WinState>();
        }
    }
}