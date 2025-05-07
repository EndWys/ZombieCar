using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startCam;
    [SerializeField] private CinemachineVirtualCamera gameplayCam;

    public void SwitchToGameplay()
    {
        startCam.Priority = 0;
        gameplayCam.Priority = 10;
    }

    public void SwitchToStart()
    {
        startCam.Priority = 10;
        gameplayCam.Priority = 0;
    }
}