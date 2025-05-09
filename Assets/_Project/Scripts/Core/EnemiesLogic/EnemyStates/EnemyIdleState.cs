using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyIdleState : EnemyWaitingForTargetState
    {
        private const float START_CHASING_RANGE = 30f;

        private CancellationTokenSource _idleTokenSource;

        public EnemyIdleState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            base.Enter();

            _stateContext.Runner.Stop();

            _idleTokenSource = new();
            WaitAndWander().Forget();
        }

        public override void Exit()
        {
            base.Exit();

            _idleTokenSource?.Cancel();
            _idleTokenSource?.Dispose();
        }

        private async UniTaskVoid WaitAndWander()
        {
            float delay = Random.Range(1f, 3f);
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _idleTokenSource.Token);

            if (_idleTokenSource.Token.IsCancellationRequested)
                return;

            _stateSwitcher.SwitchState<EnemyWanderState>();
        }
    }
}