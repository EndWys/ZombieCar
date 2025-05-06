using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Turret;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameRunState : GameState
    {
        private ICarEngineHandler _engineHandler;
        private ICarHealth _carHealth;

        private TurretController _turretController;

        private RoadFinish _roadFinish;

        public GameRunState(ICarEngineHandler carEngineHandler, ICarHealth carHealth,
            TurretController turretController, RoadFinish roadFinish)
        {
            _engineHandler = carEngineHandler;
            _carHealth = carHealth;
            _turretController = turretController;
            _roadFinish = roadFinish;
        }

        public override void Enter()
        {
            _engineHandler.StartMoving();
            _turretController.SetActive(true);

            _carHealth.OnHealthGone += OnDeath;
            _roadFinish.OnFinishReached += OnFinish;
        }

        public override void Exit()
        {
            _engineHandler.StopMoving();
            _turretController.SetActive(false);

            _carHealth.OnHealthGone -= OnDeath;
            _roadFinish.OnFinishReached -= OnFinish;
        }

        private void OnFinish()
        {
            _stateSwitcher.SwitchState<WinState>();
        }

        private void OnDeath()
        {
            _stateSwitcher.SwitchState<LoseState>();
        }
    }
}