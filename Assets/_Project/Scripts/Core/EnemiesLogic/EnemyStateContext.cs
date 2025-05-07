using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.ObjectPoolSytem;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public class EnemyStateContext
    {
        public ISelfReleaseObject PoolObject;
        public IEnemiesTarget Target;
        public EnemyRunner Runner;
        public IAttackPerformer AttackPerformer;
        public IHealthHolder EnemyHealth;

        public EnemyStateContext(ISelfReleaseObject poolObject, IEnemiesTarget target, EnemyRunner runner, IAttackPerformer attack, IHealthHolder enemyHealth)
        {
            Target = target;
            PoolObject = poolObject;
            Runner = runner;
            AttackPerformer = attack;
            EnemyHealth = enemyHealth;
        }

    }
}