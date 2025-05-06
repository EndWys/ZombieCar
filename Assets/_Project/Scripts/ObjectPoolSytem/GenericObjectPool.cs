using Assets._Project.Scripts.Core.EnemiesLogic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets._Project.Scripts.ObjectPoolSytem
{
    public interface IParentPool<TObject> where TObject : PoolObject
    {
        public void Release(TObject poolObject);
    }

    public abstract class GenericObjectPool<TObject> : MonoBehaviour where TObject : PoolObject
    {
        protected ObjectPool<TObject> _pool;

        protected abstract bool _collectionCheck { get; }
        protected abstract int _defaultCapacity { get; }

        protected void CreatePool()
        {
            _pool = new ObjectPool<TObject>(
                createFunc: CratePoolObject,
                actionOnGet: OnGetObjectToPool,
                actionOnRelease: OnReleaseObjectFromPool,
                actionOnDestroy: Destroy,
                collectionCheck: _collectionCheck,
                defaultCapacity: _defaultCapacity
            );
        }

        protected abstract TObject CratePoolObject();
        protected virtual void OnGetObjectToPool(TObject poolObject)
        {
            poolObject.OnGetFromPool();
        }
        protected virtual void OnReleaseObjectFromPool(TObject poolObject)
        {
            poolObject.OnReleaseToPool();
        }

        public virtual TObject GetObject()
        {
            return _pool.Get();
        }

        public virtual void ReleaseObject(TObject poolObject)
        {
            _pool.Release(poolObject);
        }
    }
}