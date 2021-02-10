using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ButtonScripts
{
    public class StartButton : MonoBehaviour
    {
        private Button _startGameButton;

        private void Start()
        {
            _startGameButton = GetComponent<Button>();
            _startGameButton.onClick.AddListener(StartGame);
        
            AudioManager.AudioManager.Instance.Play("Music",0.5f);
        }

        private void StartGame()
        {
            SceneManager.LoadScene("MainScene",LoadSceneMode.Single);
            AudioManager.AudioManager.Instance.Play("UI");
        }
    }
}
