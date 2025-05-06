using Assets._Project.Scripts.Core.GameManagement.StateMachine.States;
using VContainer;

public class GameStatesInstaller
{
    public static void ConfigureStates(IContainerBuilder builder)
    {
        builder.Register<WaitForTapState>(Lifetime.Transient);
        builder.Register<GameRunState>(Lifetime.Transient);
        builder.Register<WinState>(Lifetime.Transient);
        builder.Register<LoseState>(Lifetime.Transient);
    }
}
