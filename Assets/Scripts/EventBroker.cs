using System;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    public static event Action GameOverHandler;
    public static event Action RestartGameHandler;
    public static event Action ResumeGameHandler;

    public static void CallGameOver()
    {
        GameOverHandler?.Invoke();
    }

    public static void CallRestartGame()
    {
        RestartGameHandler?.Invoke();
    }

    public static void CallResumeGame()
    {
        ResumeGameHandler?.Invoke();
    }
}
