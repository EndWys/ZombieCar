using Assets._Project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public interface IParentPool<T>
    {
        public void Release(T poolObject);
    }

    public interface IParentEnemyPool : IParentPool<Enemy>
    {
    }

    public class EnemyPool : CachedMonoBehaviour, IParentEnemyPool
    {
        [SerializeField] private Enemy enemyPrefab;

        private ObjectPool<Enemy> _pool;

        [Inject]
        private void Construct(IObjectResolver _resolver)
        {
            _pool = new ObjectPool<Enemy>(
                createFunc: () =>
                {
                    var enemy = _resolver.Instantiate(enemyPrefab, CachedTrasform);
                    enemy.Init();
                    return enemy;
                },
                actionOnGet: enemy => enemy.CachedGameObject.SetActive(true),
                actionOnRelease: enemy => enemy.CachedGameObject.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: false,
                defaultCapacity: 30
            );
        }


        public Enemy Get() => _pool.Get();
        public void Release(Enemy enemy) => _pool.Release(enemy);
    }
}