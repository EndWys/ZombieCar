using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyDeadState : EnemyState
    {
        private const float DESPAWN_DISTANCE = 10F;

        private Enemy _enemy;

        public EnemyDeadState(IStateSwitcher<EnemyState> stateSwitcher, EnemyStateContext stateContext) : base(stateSwitcher, stateContext)
        {
        }

        public override void Tick()
        {
            if (_stateContext.PointRunner.RemainingDistanceToPoint(_stateContext.Target.Tr.position) >= DESPAWN_DISTANCE)
            {
                _stateSwitcher.SwitchState<EnemyDeactivatedState>();
            }
        }
    }
}