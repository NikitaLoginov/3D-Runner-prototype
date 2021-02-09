using UnityEngine;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour
{
    private Button _restartButton;

    private void Start()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        EventBroker.CallRestartGame();
    }
}
