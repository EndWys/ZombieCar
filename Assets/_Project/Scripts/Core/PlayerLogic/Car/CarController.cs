using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : CachedMonoBehaviour, ICarEngineHandler, ICarReseter
    {
        [Header("Movement Settings")]
        [SerializeField] private float speed = 5f;
        [SerializeField] private float smoothTime = 0.2f;

        [SerializeField] private AnimationCurve swayCurve;
        [SerializeField] private float swayAmplitude = 0.5f;
        [SerializeField] private float swaySpeed = 1f;

        private float _swayTime;

        private bool _isMoving;

        private Rigidbody _rigidbody;

        private Vector3 _startPos;
        private Quaternion _startRot;

        private Vector3 _currentVelocity;

        private Vector3 _forward;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPos = _rigidbody.position;
            _startRot = _rigidbody.rotation;
        }

        public void StartMoving()
        {
            _forward = CachedTrasform.forward * -1;

            _isMoving = true;
            _swayTime = 0;
        }

        public void StopMoving() => _isMoving = false;

        public void ResetSelf()
        {
            _rigidbody.position = _startPos;
            _rigidbody.rotation = _startRot;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _currentVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;

            Vector3 forwardStep = _forward * speed * Time.fixedDeltaTime;
            float baseTargetPosZ = _rigidbody.position.z + forwardStep.z;

            _swayTime += Time.fixedDeltaTime * swaySpeed;
            float swayOffset = swayCurve.Evaluate(_swayTime) * swayAmplitude;

            Vector3 finalTargetPos = new Vector3(swayOffset, 0, baseTargetPosZ);

            Vector3 smoothedPosition = Vector3.SmoothDamp(_rigidbody.position, finalTargetPos, ref _currentVelocity, smoothTime);

            Vector3 moveDirection = (smoothedPosition - _rigidbody.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection * -1, Vector3.up);
            _rigidbody.rotation = targetRotation;

            _rigidbody.MovePosition(smoothedPosition);
        }
    }
}