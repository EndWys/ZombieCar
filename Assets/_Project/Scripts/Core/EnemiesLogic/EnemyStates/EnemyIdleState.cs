namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyIdleState : EnemyState
    {
        private const float START_CHASING_RANGE = 10f;

        public EnemyIdleState(EnemyStateContext stateContext) : base(stateContext) { }

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
            if (_stateContext.Target.IsPossibleToChase() && _stateContext.Runner.RemainingDistanceToPoint(_stateContext.Target.Tr.position) <= START_CHASING_RANGE)
            {
                _stateSwitcher.SwitchState<EnemyChaseState>();
            }
            else
            {
                _stateContext.Runner.Stop();
            }    
        }
    }
}