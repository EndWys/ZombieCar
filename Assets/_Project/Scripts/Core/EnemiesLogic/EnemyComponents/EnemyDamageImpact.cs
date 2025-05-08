using Assets._Project.Scripts.Core.Common;
using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using UnityEngine;

public class EnemyDamageImpact : DamageImpact
{
    [SerializeField] private EnemyDamageable enemyDamageable;
    protected override IHealthHolder _health => enemyDamageable;

    protected override void PlayDamageSound()
    {
        SoundManager.Instance.PlayZombieHit();
    }
}
