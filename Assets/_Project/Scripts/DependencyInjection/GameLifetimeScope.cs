using Assets._Project.Scripts.Core.GameManagement;
using Assets._Project.Scripts.Core.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Scripts.DependencyInjection
{
    public class GameLifetimeScope : LifetimeScope
    {
        //[SerializeField] private PlayerController playerPrefab;
        //[SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private GameUI gameUI;

        protected override void Configure(IContainerBuilder builder)
        {
            // Game services
            //builder.Register<GameManager>(Lifetime.Singleton);

            // UI
            builder.RegisterComponent(gameUI);

            builder.RegisterEntryPoint<GameFlowInitializer>().As<ITickable>();

            // Player
            //builder.RegisterComponentInNewPrefab(playerPrefab, Lifetime.Singleton);

            // Enemies
            //builder.RegisterComponent(enemySpawner);
        }
    }
}