using Assets._Project.Scripts.Core.GameManagement;
using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
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

            builder.RegisterComponentInHierarchy<CarController>();
            builder.RegisterComponentInHierarchy<RoadFinish>();

            builder.RegisterEntryPoint<GameFlowInitializer>().As<ITickable>();
        }
    }
}