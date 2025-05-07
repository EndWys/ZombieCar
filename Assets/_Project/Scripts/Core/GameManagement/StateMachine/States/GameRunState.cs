using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Turret;
using Cysharp.Threading.Tasks;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameRunState : GameState
    {
        private ICarEngineHandler _engineHandler;
        private ICarHealth _carHealth;
        private ICarFinisher _carFinisher;

        private TurretController _turretController;

        private RoadFinish _roadFinish;

        private int _delayBeforeStartShootingMs = 2000;

        public GameRunState(ICarEngineHandler carEngineHandler, ICarHealth carHealth, ICarFinisher carFinisher,
            TurretController turretController, RoadFinish roadFinish)
        {
            _engineHandler = carEngineHandler;
            _carHealth = carHealth;
            _carFinisher = carFinisher;
            _turretController = turretController;
            _roadFinish = roadFinish;
        }

        public override async void Enter()
        {
            _carFinisher.IsOnFinish = false;
            _engineHandler.StartMoving();
            await UniTask.Delay(_delayBeforeStartShootingMs);
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
            _carFinisher.IsOnFinish = true;
            _stateSwitcher.SwitchState<WinState>();
        }

        private void OnDeath()
        {
            _stateSwitcher.SwitchState<LoseState>();
        }
    }
}