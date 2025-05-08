using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Random = UnityEngine.Random;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyWanderState : EnemyWaitingForTargetState
    {
        private const float START_CHASING_RANGE = 30f;

        private readonly float _wanderSpeedMultiplier = 0.5f;
        private float _thresholdDistance = 0.5f;
        private float _wanderRadius = 3f;

        private Vector3 _currentTarget;
        private int _pointsToWalk;
        private int _currentPointIndex;

        private CancellationTokenSource _wanderTokenSource;

        public EnemyWanderState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            base.Enter();

            _wanderTokenSource = new();
            _pointsToWalk = Random.Range(1, 4);
            _currentPointIndex = 0;
            ChooseNextPointToWalk().Forget();
        }

        private async UniTaskVoid ChooseNextPointToWalk()
        {
            while (_currentPointIndex < _pointsToWalk)
            {
                _currentTarget = GetRandomPointAroundSpawn();
                
                while (_stateContext.Runner?.RemainingDistanceToPoint(_currentTarget) > _thresholdDistance)
                {
                    await UniTask.Yield(_wanderTokenSource.Token);

                    if (_wanderTokenSource.Token.IsCancellationRequested)
                        return;
                }

                _currentPointIndex++;

                float randomDelayBetweenPoints = Random.Range(0.25f, 1f);
                await UniTask.Delay(TimeSpan.FromSeconds(randomDelayBetweenPoints), cancellationToken: _wanderTokenSource.Token);

                if (_wanderTokenSource.Token.IsCancellationRequested)
                    return;
                
            }

            if (_wanderTokenSource.Token.IsCancellationRequested)
                return;

            _stateSwitcher.SwitchState<EnemyIdleState>();
        }

        private Vector3 GetRandomPointAroundSpawn()
        {
            Vector3 center = _stateContext.Runner.CachedTrasform.position;
            float radius = _wanderRadius;

            Vector2 offset = Random.insideUnitCircle * radius;
            return new Vector3(center.x + offset.x, center.y, center.z + offset.y);
        }

        public override void Exit()
        {
            _wanderTokenSource?.Cancel();
            _wanderTokenSource?.Dispose();

            base.Exit();
        }

        public override void Tick()
        {
            WalkToWanderPoint();
            base.Tick();
        }

        private void WalkToWanderPoint()
        {
            if (_stateContext.Runner?.RemainingDistanceToPoint(_currentTarget) > _thresholdDistance)
            {
                _stateContext.Runner.RunToPoint(_currentTarget, _wanderSpeedMultiplier);
            }
            else
            {
                _stateContext.Runner.Stop();
            }
        }
    }
}