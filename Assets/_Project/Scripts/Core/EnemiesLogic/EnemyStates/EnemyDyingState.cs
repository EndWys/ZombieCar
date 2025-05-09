using Cysharp.Threading.Tasks;
using System;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyStates
{
    public abstract class EnemyDyingState : EnemyState
    {
        private double _delayBeforeDeath = 0.3f;

        protected bool _isDead = false;

        protected EnemyDyingState(EnemyStateContext stateContext) : base(stateContext) { }

        public override void Enter()
        {
            _isDead = false;
            _stateContext.EnemyHealth.OnHealthGone += Die;
        }

        protected async void Die()
        {
            _stateContext.EnemyHealth.OnHealthGone -= Die;
            _isDead = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeDeath));
            _stateSwitcher.SwitchState<EnemyDeactivatedState>();
        }
    }
}