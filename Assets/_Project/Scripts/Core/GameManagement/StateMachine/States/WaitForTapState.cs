using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class WaitForTapState : GameState
    {
        private GameUI _gameUI;

        private ICarHealth _carHealth;
        private EnemySpawner _enemySpawner;

        private CameraSwitcher _cameraSwitcher;

        public WaitForTapState(GameUI gameUI, ICarHealth carHealth, EnemySpawner enemySpawner, CameraSwitcher cameraSwitcher)
        {
            _gameUI = gameUI;
            _carHealth = carHealth;
            _enemySpawner = enemySpawner;
            _cameraSwitcher = cameraSwitcher;
        }

        public override async void Enter()
        {
            _cameraSwitcher.SwitchToStart();
            _carHealth.ResetHealth();
            _enemySpawner.DeactivateAll();
            await _gameUI.ToggleStartPanel(true);
            await _gameUI.ToggleReloadPanel(false);
            TapInput.OnTap += OnTap;
        }

        public override void Exit()
        {
            TapInput.OnTap -= OnTap;
        }

        private async void OnTap()
        {
            await _gameUI.ToggleStartPanel(false);
            _enemySpawner.Spawn();
            _cameraSwitcher.SwitchToGameplay();
            _stateSwitcher.SwitchState<GameRunState>();

        }
    }
}