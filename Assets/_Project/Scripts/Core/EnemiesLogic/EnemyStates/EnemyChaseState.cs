using Assets._Project.Scripts.StateMachine;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyChaseState : EnemyState
    {
        private float _attackDistace = 1.5f;

        public EnemyChaseState(IStateSwitcher<EnemyState> stateSwitcher, EnemyStateContext stateContext) : base(stateSwitcher, stateContext)
        {
        }

        public override void Tick()
        {
            Vector3 targetPosition = _stateContext.Target.Tr.position;

            float distance = _stateContext.PointRunner.RemainingDistanceToPoint(targetPosition);

            if (distance <= _attackDistace)
            {
                Attack();
                return;
            }

            _stateContext.PointRunner.RunToPoint(targetPosition);
        }
        
        private void Attack()
        {
            _stateContext.AttackPerformer.Attack(_stateContext.Target);
            _stateSwitcher.SwitchState<EnemyDeadState>();
        }
    }
}