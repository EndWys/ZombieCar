using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Turret;
using Assets._Project.Scripts.Core.UI.HealthBars;
using Cysharp.Threading.Tasks;
using System;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameRunState : GameState
    {
        private ICarEngineHandler _engineHandler;
        private ICarHealth _carHealth;
        private ICarFinisher _carFinisher;
        private ICarShowBar _carHealthBar;
        private ICarImpactCancelation _carImpactCancelation;

        private TurretController _turretController;

        private RoadFinish _roadFinish;

        private double _delayBeforeStartShooting = 2d;

        private double _delayBeforeDeath = 1d;

        public GameRunState(ICarEngineHandler carEngineHandler, ICarHealth carHealth, 
            ICarFinisher carFinisher, ICarShowBar carHealthBar,
            ICarImpactCancelation carImpactCancelation,
            TurretController turretController, RoadFinish roadFinish)
        {
            _engineHandler = carEngineHandler;
            _carHealth = carHealth;
            _carFinisher = carFinisher;
            _carHealthBar = carHealthBar;
            _carImpactCancelation = carImpactCancelation;
            _turretController = turretController;
            _roadFinish = roadFinish;
        }

        public override async void Enter()
        {
            _engineHandler.StartMoving();
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeStartShooting));
            _carFinisher.IsOnFinish = false;
            _turretController.SetActive(true);
            _carHealthBar.Show();

            _carHealth.OnHealthGone += OnDeath;
            _roadFinish.OnFinishReached += OnFinish;
        }

        public override void Exit()
        {
            _carImpactCancelation.CancelImpact();
        }

        private void OnFinish()
        {
            GameOverActions();

            _carFinisher.IsOnFinish = true;
            _stateSwitcher.SwitchState<WinState>();
        }

        private async void OnDeath()
        {
            GameOverActions();

            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeDeath));
            _stateSwitcher.SwitchState<LoseState>();
        }

        private void GameOverActions()
        {
            _carHealth.OnHealthGone -= OnDeath;
            _roadFinish.OnFinishReached -= OnFinish;

            _carHealthBar.Hide();
            _engineHandler.StopMoving();
            _turretController.SetActive(false);
        }
    }
}