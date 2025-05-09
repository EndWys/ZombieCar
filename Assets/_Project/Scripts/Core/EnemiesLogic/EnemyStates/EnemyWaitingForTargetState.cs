using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public abstract class EnemyWaitingForTargetState : EnemyDyingState
    {
        private const float START_CHASING_RANGE = 30f;

        protected EnemyWaitingForTargetState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Tick()
        {
            SearchingTarget();
        }

        private void SearchingTarget()
        {
            if (_isDead)
            {
                _stateContext.Runner.Stop();
                return;
            }

            Vector3 targetPosition = _stateContext.Target.GetTargetPosition();

            if (_stateContext.Target.IsPossibleToChase() && _stateContext.Runner.RemainingDistanceToPoint(targetPosition) <= START_CHASING_RANGE)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }
        }
    }
}