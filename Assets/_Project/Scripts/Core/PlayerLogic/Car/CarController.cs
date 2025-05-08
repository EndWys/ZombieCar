using Assets._Project.Scripts.Core.PlayerLogic.Car.Interfaces;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.PlayerLogic.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : CachedMonoBehaviour, ICarEngineHandler, ICarReseter
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float smoothTime = 0.2f;

        private Rigidbody _rigidbody;
        private bool _isMoving;

        private Vector3 _startPos;
        private Quaternion _startRot;

        private Vector3 _currentVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPos = transform.position;
            _startRot = transform.rotation;
        }

        public void StartMoving() => _isMoving = true;
        public void StopMoving() => _isMoving = false;

        public void ResetSelf()
        {
            transform.position = _startPos;
            transform.rotation = _startRot;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _currentVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;

            Vector3 targetPos = _rigidbody.position + (CachedTrasform.forward * -1) * speed * Time.fixedDeltaTime;

            Vector3 smoothedPosition = Vector3.SmoothDamp(_rigidbody.position, targetPos, ref _currentVelocity, smoothTime);

            _rigidbody.MovePosition(smoothedPosition);
        }
    }
}