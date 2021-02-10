using System;
using UnityEngine;

//Event broker that let's classes call events from other classes without referencing them
public class EventBroker : MonoBehaviour
{
    public static event Action GameOverHandler;
    public static event Action RestartGameHandler;
    public static event Action ResumeGameHandler;
    public static event Action LoadMainMenuHandler;
    public static event Action<int> UpdateScoreHandler;
    public static event Action<float> UpdateSpeedHandler; 

    public static event Action StopMovingObjectsHandler;

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

    public static void CallUpdateScore(int scoreToAdd)
    {
        UpdateScoreHandler?.Invoke(scoreToAdd);
    }

    public static void CallUpdateSpeed(float addToSpeed)
    {
        UpdateSpeedHandler?.Invoke(addToSpeed);
    }

    public static void CallStopMovingObjects()
    {
        StopMovingObjectsHandler?.Invoke();
    }

    public static void CallMainMenu()
    {
        LoadMainMenuHandler?.Invoke();
    }
}
