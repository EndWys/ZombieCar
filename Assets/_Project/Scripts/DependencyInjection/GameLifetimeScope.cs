using Assets._Project.Scripts.Core.EnemiesLogic;
using Assets._Project.Scripts.Core.GameInput;
using Assets._Project.Scripts.Core.GameManagement;
using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using Assets._Project.Scripts.Core.PlayerLogic.Car;
using Assets._Project.Scripts.Core.PlayerLogic.Turret;
using Assets._Project.Scripts.Core.PlayerLogic.Turret.Bullet;
using Assets._Project.Scripts.Core.UI.HealthBars;
using VContainer;
using VContainer.Unity;

namespace Assets._Project.Scripts.DependencyInjection
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            UIInstaller.ConfigureUI(builder);

            builder.RegisterComponentInHierarchy<SoundManager>();

            builder.RegisterComponentInHierarchy<CameraSwitcher>();

            builder.RegisterComponentInHierarchy<CarController>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<CarAttackTarget>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<CarHealthBar>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<CarDamageImpact>().AsImplementedInterfaces();

            builder.RegisterComponentInHierarchy<BulletPool>();
            builder.RegisterComponentInHierarchy<TurretInput>();
            builder.RegisterComponentInHierarchy<TurretController>();

            builder.RegisterComponentInHierarchy<RoadFinish>();

            builder.RegisterComponentInHierarchy<EnemySpawner>();
            builder.RegisterComponentInHierarchy<EnemyPool>().AsImplementedInterfaces().AsSelf();

            GameStatesInstaller.ConfigureStates(builder);

            builder.RegisterEntryPoint<GameFlowInitializer>().As<ITickable>();
        }
    }
}