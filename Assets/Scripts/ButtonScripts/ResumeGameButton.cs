using UnityEngine;
using UnityEngine.UI;

namespace ButtonScripts
{
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
        
            AudioManager.AudioManager.Instance.Play("UI");
        }
    }
}
