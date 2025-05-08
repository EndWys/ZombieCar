using Cysharp.Threading.Tasks;
using System;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public abstract class EnemyDyingState : EnemyState
    {
        private double _delayBeforeDeath = 0.25f; 

        protected EnemyDyingState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            _stateContext.EnemyHealth.OnHealthGone += Die;
        }

        public override void Exit()
        {
            _stateContext.EnemyHealth.OnHealthGone -= Die;
        }

        public async void Die()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeDeath));
            _stateSwitcher.SwitchState<EnemyDeactivatedState>();
        }
    }
}