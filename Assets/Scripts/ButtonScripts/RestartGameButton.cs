using UnityEngine;
using UnityEngine.UI;

namespace ButtonScripts
{
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
        
            AudioManager.AudioManager.Instance.Play("UI");
        }
    }
}
