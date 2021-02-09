using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGameButton : MonoBehaviour
{
    private Button _resumeGameButton;

    private void Start()
    {
        _resumeGameButton = GetComponent<Button>();
        _resumeGameButton.onClick.AddListener(ResumeGame);
    }

    private void ResumeGame()
    {
        EventBroker.CallResumeGame();
    }
}
