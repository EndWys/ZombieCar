using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyChaseState : EnemyDyingState
    {
        private const float DESPAWN_DISTANCE = 5F;

        private float _chaseSpeedMultiplier = 1;
        private float _attackDistace = 0.5f;

        public EnemyChaseState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Tick()
        {
            if (_isDead || !_stateContext.Target.IsPossibleToChase())
            {
                _stateContext.Runner.Stop();
                return;
            }

            Vector3 currenctEnemyPosition = _stateContext.Runner.CachedTrasform.position;
            Vector3 targetPosition = _stateContext.Target.GetClosestTargetPoint(currenctEnemyPosition);

            float distance = _stateContext.Runner.RemainingDistanceToPoint(targetPosition);

            if (distance <= _attackDistace)
            {
                Attack();
                return;
            }

            _stateContext.Runner.RunToPoint(targetPosition, _chaseSpeedMultiplier);

            if (_stateContext.Runner.IsRunnerBehind(targetPosition, DESPAWN_DISTANCE))
            {
                _stateSwitcher.SwitchState<EnemyDeactivatedState>();
            }
        }
        
        private void Attack()
        {
            _stateContext.AttackPerformer.Attack(_stateContext.Target);
            _stateContext.EnemyHealth.TackeAttack(_stateContext.EnemyHealth.CurrentHealth);
        }
    }
}