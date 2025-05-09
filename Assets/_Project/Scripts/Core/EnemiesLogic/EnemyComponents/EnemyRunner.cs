using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IPointRunner
    {
        public void RunToPoint(Vector3 targetPoint, float speedMultiplier);
        public float RemainingDistanceToPoint(Vector3 targetPoint);
        public bool IsRunnerBehind(Vector3 targetPoint, float allowedDifference);
        public void Stop();
    }

    public class EnemyRunner : CachedMonoBehaviour, IPointRunner
    {
        [SerializeField] private float runSpeed = 1f;
        [SerializeField] private float acceleration = 3f;
        [SerializeField] private float rotationSpeed = 5f;

        private IMoveAnimator _animator;

        private float _currentSpeedMultiplier = 0f;
        private float _targetSpeedMultiplier = 0f;

        public void Init(IMoveAnimator animator)
        {
            _animator = animator;
        }

        public void RunToPoint(Vector3 targetPoint, float speedMultiplier)
        {
            _targetSpeedMultiplier = speedMultiplier;

            Vector3 directionToTarget = (targetPoint - CachedTrasform.position).normalized;

            RotateToTarget(directionToTarget);

            CachedTrasform.position += CachedTrasform.forward * runSpeed * _currentSpeedMultiplier * Time.deltaTime;
        }

        private void RotateToTarget(Vector3 directionToTarget)
        {
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                CachedTrasform.rotation = Quaternion.Slerp(
                    CachedTrasform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        private void FixedUpdate()
        {
            ChangingCurrentSpeed();
        }

        private void ChangingCurrentSpeed()
        {
            if (_targetSpeedMultiplier != _currentSpeedMultiplier)
            {
                _currentSpeedMultiplier = Mathf.MoveTowards(
                _currentSpeedMultiplier,
                _targetSpeedMultiplier,
                acceleration * Time.deltaTime);

                _animator.SetMoveVelocity(_currentSpeedMultiplier);
            }
        }

        public void Stop()
        {
            _targetSpeedMultiplier = 0f;
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
