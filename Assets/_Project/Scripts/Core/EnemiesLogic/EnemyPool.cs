using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public interface IParentEnemyPool : IParentPool<Enemy> { }

    public class EnemyPool : GenericObjectPool<Enemy>, IParentEnemyPool
    {
        [SerializeField] private Enemy enemyPrefab;

        protected override bool _collectionCheck => false;
        protected override int _defaultCapacity => 30;

        private IObjectResolver _resolver;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;

            CreatePool();
        }

        protected override Enemy CratePoolObject()
        {
            var enemy = _resolver.Instantiate(enemyPrefab, transform);
            enemy.Init();
            enemy.OnCreate();
            return enemy;
        }

        public void Release(Enemy poolObject)
        {
            ReleaseObject(poolObject);
        }
    }
}