using UnityEngine;
using UnityEngine.UI;

namespace ButtonScripts
{
    public class QuitButton : MonoBehaviour
    {
        private Button _quitButton;

        private void Start()
        {
            _quitButton = GetComponent<Button>();
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            AudioManager.AudioManager.Instance.Play("UI");
            Application.Quit();
        }
    }
}
