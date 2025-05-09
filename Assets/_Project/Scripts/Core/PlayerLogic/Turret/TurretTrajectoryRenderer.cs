using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Turret
{
    public interface ITrajectoryShow
    {
        public void SetActive(bool active);
    }

    [RequireComponent(typeof(LineRenderer))]
    public class TurretTrajectoryRenderer : CachedMonoBehaviour, ITrajectoryShow
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private float maxDistance = 20f;

        private LineRenderer _lineRenderer;

        public bool _isShown = false;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetActive(bool active)
        {
            _isShown = active;
        }

        private void Update()
        {
            UpdateLaser();
        }

        private void UpdateLaser()
        {
            CachedGameObject.SetActive(_isShown);

            if (!_isShown)
                return;

            Vector3 start = firePoint.position;
            Vector3 direction = firePoint.forward;
            Vector3 end = start + direction * maxDistance;

            if (Physics.Raycast(start, direction, out RaycastHit hit, maxDistance, hitMask))
            {
                end = hit.point;
            }

            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(1, end);
        }
    }
}