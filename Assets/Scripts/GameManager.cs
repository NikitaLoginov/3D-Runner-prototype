using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    private bool _isGamePaused;
    private bool _isGameOver;
    private float _spawnRate;
    private Vector3[] xSpawnPositions;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _objectPooler;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseMenuScreen;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;

    private void Awake()
    {
        xSpawnPositions = new[] {new Vector3(0, 0, 40), new Vector3(3, 0, 40), new Vector3(-3, 0, 40)};
        
        Instantiate(_player, _player.transform.position, Quaternion.identity);
        Instantiate(_objectPooler, this.transform);
    }

    private void Start()
    {
        _isGameOver = false;
        Time.timeScale = 1f;
        
        //Subscribing to Events
        EventBroker.GameOverHandler += GameOver;
        EventBroker.RestartGameHandler += RestartGame;
        EventBroker.ResumeGameHandler += ResumeGame;
        EventBroker.UpdateScoreHandler += UpdateScore;
        EventBroker.LoadMainMenuHandler += LoadMainMenu;

        _scoreText.text = "Score: " + 0;
        InvokeRepeating(nameof(SpawnObstacle), 1.0f, 2.0f);
        InvokeRepeating(nameof(SpawnCoin), 0.5f,0.2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
                ResumeGame();
            else if(!_isGamePaused)
                Pause();
        }
    }

    //Method that resumes game after it being paused
    private void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
        _pauseMenuScreen.gameObject.SetActive(false);
        
        AudioManager.AudioManager.Instance.Volume("Music",0.5f);
    }
    //Method that pauses game
    private void Pause()
    {
        Time.timeScale = 0f;
        _isGamePaused = true;
        _pauseMenuScreen.gameObject.SetActive(true);
        
        //AudioManager.Instance.Stop("Music");
        AudioManager.AudioManager.Instance.Volume("Music", 0.2f);
    }

    // Pooling obstacles from objects pool and spawning them in scene
    private void SpawnObstacle()
    {
        if (_isGameOver) return;
        
        GameObject pooledObstacle = ObjectPooler.Instance.GetPooledObstacle();
        if (pooledObstacle != null)
        {
            pooledObstacle.SetActive(true);
            pooledObstacle.transform.position = GetSpawnPosition();
        }
    }

    // Pooling coins from objects pool and spawning them in scene
    private void SpawnCoin()
    {
        if (_isGameOver) return;
        
        GameObject pooledCoin = ObjectPooler.Instance.GetPooledCoin();
        if (pooledCoin != null)
        {
            pooledCoin.SetActive(true);
            pooledCoin.transform.position = new Vector3(GetSpawnPosition().x, 1f, GetSpawnPosition().z);
        }
    }
    //Calculating random spawn position
    private Vector3 GetSpawnPosition()
    {
        Random rnd = new Random();
        int sp = rnd.Next(xSpawnPositions.Length);
        Vector3 spawnPosition = xSpawnPositions[sp];
        return spawnPosition;
    }

    //Method to update score
    private void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = "Score: " + _score;
        
        //increases speed by 5 every 30 points
        if (CheckIfRaiseDifficulty(_score))
        {
            EventBroker.CallUpdateSpeed(5f);
            AudioManager.AudioManager.Instance.Play("Difficulty");
        }
    }

    bool CheckIfRaiseDifficulty(int score)
    {
        if (score % 30 == 0)
            return true;
        return false;
    }

    //Method that fires when game is over
    private void GameOver()
    {
        _isGameOver = true;
        EventBroker.CallStopMovingObjects();
        
        _gameOverScreen.gameObject.SetActive(true);
        AudioManager.AudioManager.Instance.Stop("Music");
    }

    //Method to restart game
    private void RestartGame()
    {
        Unsubscribe();

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        
        AudioManager.AudioManager.Instance.Play("Music",0.5f);
    }


    private void LoadMainMenu()
    {
        Unsubscribe();
        
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
    private void Unsubscribe()
    {
        //Unsubscribing from events
        EventBroker.GameOverHandler -= GameOver;
        EventBroker.RestartGameHandler -= RestartGame;
        EventBroker.ResumeGameHandler -= ResumeGame;
        EventBroker.LoadMainMenuHandler -= LoadMainMenu;
        EventBroker.UpdateScoreHandler -= UpdateScore;
    }
    
}
