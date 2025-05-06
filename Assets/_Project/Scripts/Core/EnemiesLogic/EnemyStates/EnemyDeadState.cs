namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyDeadState : EnemyState
    {
        private const float DESPAWN_DISTANCE = 10F;

        private Enemy _enemy;

        public EnemyDeadState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Tick()
        {
            if (_stateContext.PointRunner.IsRunnerBehind(_stateContext.Target.Tr.position, DESPAWN_DISTANCE))
            {
                _stateSwitcher.SwitchState<EnemyDeactivatedState>();
            }
        }
    }
}