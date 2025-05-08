using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyDeadState : EnemyState
    {
        private const float DESPAWN_DISTANCE = 4F;

        private Enemy _enemy;

        public EnemyDeadState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Tick()
        {
            Vector3 carPosition = _stateContext.Target.GetTargetPosition();

            if (_stateContext.Runner.IsRunnerBehind(carPosition, DESPAWN_DISTANCE))
            {
                _stateSwitcher.SwitchState<EnemyDeactivatedState>();
            }
        }
    }
}