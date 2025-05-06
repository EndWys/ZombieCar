using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyIdleState : EnemyState
    {
        private const float START_CHASING_RANGE = 10f;

        public EnemyIdleState(IStateSwitcher<EnemyState> stateSwitcher, EnemyStateContext stateContext) : base(stateSwitcher, stateContext)
        {
        }

        public override void Tick()
        {
            if (_stateContext.PointRunner.RemainingDistanceToPoint(_stateContext.Target.Tr.position) <= START_CHASING_RANGE)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }
        }
    }
}