using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Utilities;
using UnityEngine;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IAttackPerformer
    {
        public void Attack(IEnemiesTarget target);
    }
    public class EnemyFighter : CachedMonoBehaviour, IAttackPerformer
    {
        [SerializeField] private int damage = 10;

        public void Attack(IEnemiesTarget target)
        {
            target.TackeAttack(damage);
        }
    }
}