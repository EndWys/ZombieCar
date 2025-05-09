using UnityEngine;

public interface IMoveAnimator
{
    public void SetMoveVelocity(float velocityModifier);
}
public class EnemyAnimator : MonoBehaviour, IMoveAnimator
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
