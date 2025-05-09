using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Turret
{
    [RequireComponent(typeof(LineRenderer))]
    public class TurretTrajectoryRenderer : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private float maxDistance = 20f;

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            UpdateLaser();
        }

        private void UpdateLaser()
        {
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