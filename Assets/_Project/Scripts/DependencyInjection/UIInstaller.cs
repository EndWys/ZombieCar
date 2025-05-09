using Assets._Project.Scripts.Core.UI.Panels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class UIInstaller : MonoBehaviour
{
    public static void ConfigureUI(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<StartUIPanel>();
        builder.RegisterComponentInHierarchy<WinUIPanel>();
        builder.RegisterComponentInHierarchy<LoseUIPanel>();
        builder.RegisterComponentInHierarchy<ReloadUIPanel>();
    }
}
