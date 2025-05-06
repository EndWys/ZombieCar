using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.ObjectPoolSytem;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public class EnemyStateContext
    {
        public ISelfReleaseObject PoolObject;
        public IEnemiesTarget Target;
        public IPointRunner PointRunner;
        public IAttackPerformer AttackPerformer;
        public IHealthHolder EnemyHealth;

        public EnemyStateContext(ISelfReleaseObject poolObject, IEnemiesTarget target, IPointRunner pointRunner, IAttackPerformer attack, IHealthHolder enemyHealth)
        {
            Target = target;
            PoolObject = poolObject;
            PointRunner = pointRunner;
            AttackPerformer = attack;
            EnemyHealth = enemyHealth;
        }

    }
}