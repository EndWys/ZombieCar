using Assets._Project.Scripts.ObjectPoolSytem;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Turret.Bullet
{
    public class BulletPool : GenericObjectPool<Bullet>
    {
        [SerializeField] private Bullet bulletPrefab;

        protected override bool _collectionCheck => false;

        protected override int _defaultCapacity => 5;

        private void Awake()
        {
            CreatePool();
        }

        protected override Bullet CratePoolObject()
        {
            var bullet = Instantiate(bulletPrefab, transform);
            bullet.OnExplosion += _pool.Release;
            return bullet;
        }

        protected override void OnReleaseObjectToPool(Bullet poolObject)
        {
            poolObject.OnExplosion -= _pool.Release;
            base.OnReleaseObjectToPool(poolObject);
        }
    }
}