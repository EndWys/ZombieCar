using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Turret;
using Assets._Project.Scripts.Core.UI;
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

        private TurretController _turretController;

        private RoadFinish _roadFinish;

        private double _delayBeforeStartShooting = 2d;

        private double _delayBeforeDeath = 0.25d;

        public GameRunState(ICarEngineHandler carEngineHandler, ICarHealth carHealth, 
            ICarFinisher carFinisher, ICarShowBar carHealthBar,
            TurretController turretController, RoadFinish roadFinish)
        {
            _engineHandler = carEngineHandler;
            _carHealth = carHealth;
            _carFinisher = carFinisher;
            _carHealthBar = carHealthBar;
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
            _engineHandler.StopMoving();
            _turretController.SetActive(false);
            _carHealthBar.Hide();

            _carHealth.OnHealthGone -= OnDeath;
            _roadFinish.OnFinishReached -= OnFinish;
        }

        private void OnFinish()
        {
            _carFinisher.IsOnFinish = true;
            _stateSwitcher.SwitchState<WinState>();
        }

        private async void OnDeath()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeDeath));
            _stateSwitcher.SwitchState<LoseState>();
        }
    }
}