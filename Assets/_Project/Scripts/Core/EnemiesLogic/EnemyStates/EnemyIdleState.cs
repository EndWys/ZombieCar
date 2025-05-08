using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Random = UnityEngine.Random;


namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyIdleState : EnemyState
    {
        private const float START_CHASING_RANGE = 30f;

        private CancellationTokenSource _idleTokenSource;

        public EnemyIdleState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            _stateContext.Runner.Stop();

            _idleTokenSource = new();
            WaitAndWander().Forget();

            _stateContext.EnemyHealth.OnHealthGone += Die;
        }

        private async UniTaskVoid WaitAndWander()
        {
            float delay = Random.Range(1f, 3f);
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _idleTokenSource.Token);

            if (_idleTokenSource.Token.IsCancellationRequested)
                return;

            _stateSwitcher.SwitchState<EnemyWanderState>();
        }
        private void Die()
        {
            _stateSwitcher.SwitchState<EnemyDeactivatedState>();
        }

        public override void Exit()
        {
            _stateContext.EnemyHealth.OnHealthGone -= Die;
        }


        public override void Tick()
        {
            if (_stateContext.Target.IsPossibleToChase() && _stateContext.Runner.RemainingDistanceToPoint(_stateContext.Target.Tr.position) <= START_CHASING_RANGE)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }  
        }
    }
}