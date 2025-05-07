using UnityEngine;

namespace Assets._Project.Scripts.Core.GameInput
{
    public class TurretInput : MonoBehaviour
    {
        [field: SerializeField] public float MaxRotationAngle { get; private set; } = 60f;
        [SerializeField] private float sensitivity = 0.2f;

        public float CurrentRotation { get; private set; }

        private float _accumulatedRotation = 0f;
        private Vector2 _lastInputPos;
        private bool _isInputActive;

        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                _isInputActive = true;
                _lastInputPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isInputActive = false;
            }

            if (_isInputActive)
            {
                Vector2 currentPos = Input.mousePosition;
                float deltaX = currentPos.x - _lastInputPos.x;

                ApplyInput(deltaX);

                _lastInputPos = currentPos;
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _isInputActive = true;
                    _lastInputPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isInputActive = false;
                }

                if (_isInputActive && touch.phase == TouchPhase.Moved)
                {
                    float deltaX = touch.position.x - _lastInputPos.x;

                    ApplyInput(deltaX);

                    _lastInputPos = touch.position;
                }
            }
            else
            {
                _isInputActive = false;
            }
#endif
        }

        private void ApplyInput(float deltaX)
        {
            _accumulatedRotation += deltaX * sensitivity;

            _accumulatedRotation = Mathf.Clamp(_accumulatedRotation, -MaxRotationAngle, MaxRotationAngle);
            CurrentRotation = _accumulatedRotation;
        }
    }
}