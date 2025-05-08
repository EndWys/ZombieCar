using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.ObjectPoolSytem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Turret.Bullet
{
    public class Bullet : PoolObject
    {
        [SerializeField] private int damage = 50;
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifetime = 2f;

        private float _timer;

        public event Action<Bullet> OnExplosion;

        private void Update()
        {
            CachedTrasform.position += CachedTrasform.forward * speed * Time.deltaTime;
            _timer += Time.deltaTime;

            if (_timer >= lifetime)
                OnExplosion?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyDamageable damageable))
            {
                damageable.TackeAttack(damage);
                OnExplosion?.Invoke(this);
            }
        }

        public override void OnGetFromPool()
        {
            _timer = 0f;
            CachedGameObject.SetActive(true);
        }

        public override void OnReleaseToPool()
        {
            CachedGameObject.SetActive(false);
        }
    }
}