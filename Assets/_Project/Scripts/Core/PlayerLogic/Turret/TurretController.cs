using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.PlayerLogic.Turret.Bullet;
using UnityEngine;
using VContainer;

namespace Assets._Project.Scripts.Core.PlayerLogic.Turret
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private Transform turretPivot;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private float fireCooldown = 0.3f;
        
        private BulletPool _bulletPool;

        private TurretInput _input;
        private float _lastFireTime;
        private bool _active;

        [Inject]
        public void Construct(TurretInput input, BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _input = input;
        }

        public void SetActive(bool active)
        {
            _active = active;
        }

        void Update()
        {
            if (!_active || _input == null) return;

            RotateTurret();

            if (Time.time - _lastFireTime >= fireCooldown)
            {
                Fire();
                _lastFireTime = Time.time;
            }
        }

        private void RotateTurret()
        {
            float targetYRotation = _input.CurrentRotation;
            Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);

            turretPivot.rotation = Quaternion.Slerp(turretPivot.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        private void Fire()
        {
            var bullet = _bulletPool.GetObject();
            bullet.CachedTrasform.position = firePoint.position;
            bullet.CachedTrasform.rotation = turretPivot.rotation;
            bullet.Init();
        }
    }
}