using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;
using VContainer;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public class Enemy : PoolObject, ISelfReleaseObject
    {
        [SerializeField] private EnemyRunner enemyRunner;
        [SerializeField] private EnemyFighter enemyAttack;
        [SerializeField] private EnemyDamageable enemyDamageable;
        [SerializeField] private EnemyAnimator animator;

        private EnemyAI _ai;

        private IEnemiesTarget _target;
        private IParentEnemyPool _pool;

        private Quaternion _starterRotation = new Quaternion(0, 180, 0, 0);

        [Inject]
        private void Construct(IEnemiesTarget target, IParentEnemyPool pool)
        {
            _target = target;
            _pool = pool;

            enemyRunner.Init(animator);
        }

        public void Init()
        {
            EnemyStateContext stateContext = new EnemyStateContext(this, _target, enemyRunner, enemyAttack, enemyDamageable);
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

        public override void OnCreate() => OnSpawn();
        public override void OnGetFromPool() => OnSpawn();
        private void OnSpawn()
        {
            enemyDamageable.ResetHealth();
            CachedTrasform.rotation = _starterRotation;
            CachedGameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            CachedGameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            //To cancel async tasks in states
            _ai.Deactivate();
        }

    }
}