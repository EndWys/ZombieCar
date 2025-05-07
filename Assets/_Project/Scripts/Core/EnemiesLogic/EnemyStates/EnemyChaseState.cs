using Assets._Project.Scripts.StateMachine;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyChaseState : EnemyState
    {
        private float _attackDistace = 1.5f;

        public EnemyChaseState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            _stateContext.EnemyHealth.OnHealthGone += Die;
        }

        public override void Exit()
        {
            _stateContext.EnemyHealth.OnHealthGone -= Die;
        }

        public void Die()
        {
            _stateSwitcher.SwitchState<EnemyDeactivatedState>();
        }

        public override void Tick()
        {
            if (!_stateContext.Target.IsPossibleToChase())
            {
                _stateSwitcher.SwitchState<EnemyIdleState>();
                return;
            }

            Vector3 targetPosition = _stateContext.Target.Tr.position;

            float distance = _stateContext.Runner.RemainingDistanceToPoint(targetPosition);

            if (distance <= _attackDistace)
            {
                Attack();
                return;
            }

            _stateContext.Runner.RunToPoint(targetPosition);
        }
        
        private void Attack()
        {
            _stateContext.AttackPerformer.Attack(_stateContext.Target);
            _stateSwitcher.SwitchState<EnemyDeactivatedState>();
        }
    }
}