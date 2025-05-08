using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IPointRunner
    {
        public void RunToPoint(Vector3 targetPoint, float speedMultiplier);
        public float RemainingDistanceToPoint(Vector3 targetPoint);
        public bool IsRunnerBehind(Vector3 targetPoint, float allowedDifference);
    }
    public class EnemyRunner : CachedMonoBehaviour, IPointRunner
    {
        [SerializeField] private float runSpeed;

        private EnemyAnimator _animator;

        public void Init(EnemyAnimator animator)
        {
            _animator = animator;
        }

        public void RunToPoint(Vector3 targetPoint, float speedMultiplier)
        {
            Vector3 direction = (targetPoint - CachedTrasform.position).normalized;

            float actualSpeed = runSpeed * speedMultiplier;

            CachedTrasform.position += direction * actualSpeed * Time.deltaTime * speedMultiplier;
            CachedTrasform.forward = Vector3.Lerp(CachedTrasform.forward, direction, 0.1f);

            _animator.SetMoveVelocity(speedMultiplier);
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