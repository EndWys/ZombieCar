using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Utilities;

namespace Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents
{
    public interface IAttackPerformer
    {
        public void Attack(IEnemiesTarget target);
    }
    public class EnemyFighter : CachedMonoBehaviour, IAttackPerformer
    {
        public void Attack(IEnemiesTarget target)
        {
            target.TackeAttack();
        }
    }
}