using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private Button _mainMenuButton;

    private void Awake()
    {
        _mainMenuButton = GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenuScene");
    }
}
