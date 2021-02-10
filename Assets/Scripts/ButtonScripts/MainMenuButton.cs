using UnityEngine;
using UnityEngine.UI;

namespace ButtonScripts
{
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
            EventBroker.CallMainMenu();
        
            AudioManager.AudioManager.Instance.Play("UI");
        }
    }
}
