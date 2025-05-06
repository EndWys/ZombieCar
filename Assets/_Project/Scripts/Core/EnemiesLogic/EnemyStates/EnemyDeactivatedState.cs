using Assets._Project.Scripts.StateMachine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public class EnemyDeactivatedState : EnemyState
    {
        public EnemyDeactivatedState(IStateSwitcher<EnemyState> stateSwitcher, EnemyStateContext stateContext) : base(stateSwitcher, stateContext)
        {
        }

        public override void Enter()
        {
            _stateContext.PoolObject.SelfRelease();
        }
    }
}