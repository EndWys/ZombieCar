using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _animIDVelocity;

    private void Awake()
    {
        _animIDVelocity = Animator.StringToHash("Velocity");
    }

    public void SetMoveVelocity(float velocityModifier)
    {
        _animator.SetFloat(_animIDVelocity, velocityModifier);
    }
}
