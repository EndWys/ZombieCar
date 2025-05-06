using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Utilities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : CachedMonoBehaviour, ICarEngineHandler, ICarReseter
{
    [SerializeField] private float speed = 5f;

    private Rigidbody _rigidbody;
    private bool _isMoving;

    private Vector3 _startPos;
    private Quaternion _startRot;

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
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;

        Vector3 forward = CachedTrasform.forward * -1;
        _rigidbody.MovePosition(_rigidbody.position + forward * speed * Time.fixedDeltaTime);
    }
}
