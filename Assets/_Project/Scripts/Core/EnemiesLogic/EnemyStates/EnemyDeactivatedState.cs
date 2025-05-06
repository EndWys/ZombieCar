namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyDeactivatedState : EnemyState
    {
        public EnemyDeactivatedState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            _stateContext.PoolObject.SelfRelease();
        }
    }
}