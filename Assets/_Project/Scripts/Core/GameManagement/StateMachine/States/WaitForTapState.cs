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

        public override void Enter()
        {
            _cameraSwitcher.SwitchToStart();
            _carHealth.ResetHealth();
            _enemySpawner.DeactivateAll();
            _gameUI.ToggleStartPanel(true);
            TapInput.OnTap += OnTap;
        }

        public override void Exit()
        {
            TapInput.OnTap -= OnTap;
        }

        private void OnTap()
        {
            _enemySpawner.Spawn();
            _gameUI.ToggleStartPanel(false);
            _stateSwitcher.SwitchState<GameRunState>();
            _cameraSwitcher.SwitchToGameplay();
        }
    }
}