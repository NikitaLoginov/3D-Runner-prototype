using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _startGameButton;

    private void Start()
    {
        _startGameButton = GetComponent<Button>();
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene("MainScene");
    }
}
