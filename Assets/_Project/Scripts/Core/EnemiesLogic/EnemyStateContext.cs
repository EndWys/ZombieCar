using Assets._Project.Scripts.Core.EnemiesLogic.EnemyComponents;
using Assets._Project.Scripts.Core.PlayerLogic;

namespace Assets._Project.Scripts.Core.EnemiesLogic
{
    public class EnemyStateContext
    {
        public IPoolObject PoolObject;
        public IEnemiesTarget Target;
        public IPointRunner PointRunner;
        public IAttackPerformer AttackPerformer;

        public EnemyStateContext(IPoolObject poolObject, IEnemiesTarget target, IPointRunner pointRunner, IAttackPerformer attack)
        {
            Target = target;
            PoolObject = poolObject;
            PointRunner = pointRunner;
            AttackPerformer = attack;
        }

    }
}