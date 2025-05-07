using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IPointRunner
    {
        public void RunToPoint(Vector3 targetPoint);
        public float RemainingDistanceToPoint(Vector3 targetPoint);
        public bool IsRunnerBehind(Vector3 targetPoint, float allowedDifference);
    }
    public class EnemyRunner : CachedMonoBehaviour, IPointRunner
    {
        [SerializeField] private float runSpeed;

        private EnemyAnimator _animator;
        private float _runSpeedMultiplier = 1f;

        public void Init(EnemyAnimator animator)
        {
            _animator = animator;
        }

        public void RunToPoint(Vector3 targetPoint)
        {
            Vector3 direction = (targetPoint - CachedTrasform.position).normalized;

            CachedTrasform.position += direction * runSpeed * Time.deltaTime;

            CachedTrasform.forward = Vector3.Lerp(CachedTrasform.forward, direction, 0.1f);

            _animator.SetMoveVelocity(_runSpeedMultiplier);
        }

        public void Stop()
        {
            _animator.SetMoveVelocity(0);
        }

        public float RemainingDistanceToPoint(Vector3 targetPoint)
        {
            return Vector3.Distance(CachedTrasform.position, targetPoint);
        }

        public bool IsRunnerBehind(Vector3 targetPoint, float allowedDifference)
        {
            return CachedTrasform.position.z + allowedDifference < targetPoint.z;
        }
    }
}