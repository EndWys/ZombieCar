using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.Core.PlayerLogic;
using Assets._Project.Scripts.Utilities;
using UnityEngine;
using VContainer;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public interface IPoolObject
    {
        public void SelfRelease();
    }

    public class Enemy : CachedMonoBehaviour, IPoolObject
    {
        [SerializeField] private EnemyRunner enemyRunner;
        [SerializeField] private EnemyFighter enemyAttack;

        private EnemyAI _ai;

        private IEnemiesTarget _target;
        private IParentEnemyPool _pool;

        [Inject]
        private void Construct(IEnemiesTarget target, IParentEnemyPool pool)
        {
            _target = target;
            _pool = pool;
        }

        public void Init()
        {
            EnemyStateContext stateContext = new EnemyStateContext(this, _target, enemyRunner, enemyAttack);
            _ai = new EnemyAI();
            _ai.Init(stateContext);
        }

        private void FixedUpdate()
        {
            _ai.Tick();
        }

        public void Activate()
        {
            _ai.Activate();
        }

        public void Deactivate()
        {
            _ai.Deactivate();
        }

        public void SelfRelease()
        {
            _pool.Release(this);
        }
    }
}