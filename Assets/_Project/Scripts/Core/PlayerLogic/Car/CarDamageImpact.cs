using Assets._Project.Scripts.Core.Common;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using UnityEngine;
public interface ICarImpactCancelation 
{ 
    public void CancelImpact(); 
}
public class CarDamageImpact : DamageImpact, ICarImpactCancelation
{
    [SerializeField] private CarAttackTarget carHealth;
    protected override IHealthHolder _health => carHealth;

    protected override void PlayDamageSound()
    {
        SoundManager.Instance.PlayCarDamage();
    }

    protected override void PlayDeathSound()
    {
        SoundManager.Instance.PlayCarExplosion();
    }
}
