﻿using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.UI.Panels;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WaitForTapState : GameState
    {
        private StartUIPanel _startUIPanel;
        private ReloadUIPanel _reloadUIPanel;

        private ICarHealth _carHealth;
        private EnemySpawner _enemySpawner;

        private CameraSwitcher _cameraSwitcher;

        public WaitForTapState(StartUIPanel startPanel, ReloadUIPanel reloadUIPanel,
            ICarHealth carHealth, EnemySpawner enemySpawner, CameraSwitcher cameraSwitcher)
        {
            _startUIPanel = startPanel;
            _reloadUIPanel = reloadUIPanel;

            _carHealth = carHealth;
            _enemySpawner = enemySpawner;
            _cameraSwitcher = cameraSwitcher;
        }

        public override async void Enter()
        {
            _cameraSwitcher.SwitchToStart();
            _carHealth.ResetHealth();
            _enemySpawner.DeactivateAll();

            _enemySpawner.Spawn();

            await _startUIPanel.Show();
            await _reloadUIPanel.Hide();

            TapInput.OnTap += OnTap;
        }

        private async void OnTap()
        {
            TapInput.OnTap -= OnTap;

            await _startUIPanel.Hide();

            _cameraSwitcher.SwitchToGameplay();
            _stateSwitcher.SwitchState<GameRunState>();
        }
    }
}