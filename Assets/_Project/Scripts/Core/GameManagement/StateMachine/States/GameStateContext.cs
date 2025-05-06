using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Interfaces;
using Assets._Project.Scripts.Core.UI;

namespace Assets._Project.Scripts.Core.GameManagement.StateMachine.States
{
    public class GameStateContext
    {
        public GameUI UI;
        public ICarEngineHandler CarEngine;
        public ICarReseter CarReseter;
        public IHealthHolder CarHealth;
        public RoadFinish RoadFinish;
        public EnemySpawner EnemySpawner;

        public GameStateContext(GameUI uI, ICarEngineHandler carEngine, ICarReseter carReseter, IHealthHolder carHealth, RoadFinish roadFinish, EnemySpawner enemySpawner)
        {
            UI = uI;
            CarEngine = carEngine;
            CarReseter = carReseter;
            RoadFinish = roadFinish;
            EnemySpawner = enemySpawner;
            CarHealth = carHealth;
        }
    }
}