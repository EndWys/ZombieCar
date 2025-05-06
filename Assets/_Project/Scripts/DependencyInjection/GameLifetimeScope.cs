using Assets._Project.Scripts.Core.EnemiesLogic;
using Assets._Project.Scripts.Core.GameManagement;
using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic;
using Assets._Project.Scripts.Core.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Scripts.DependencyInjection
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameUI gameUI;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(gameUI);

            builder.RegisterComponentInHierarchy<CarController>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<CarAttackTarget>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<RoadFinish>();
            builder.RegisterComponentInHierarchy<EnemySpawner>();
            builder.RegisterComponentInHierarchy<EnemyPool>().As<IParentEnemyPool>().AsSelf();

            builder.RegisterEntryPoint<GameFlowInitializer>().As<ITickable>();
        }
    }
}