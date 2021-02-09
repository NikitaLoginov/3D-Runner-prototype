using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    private static bool _isGameOver;
    public static bool IsGameOver
    {
        get { return _isGameOver; }
    }
    private float _spawnRate;
    private Vector3[] xSpawnPositions;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _objectPooler;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseMenuScreen;
    private bool _gameIsPaused;

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
        
        //Events
        EventBroker.GameOverHandler += GameOver;
        EventBroker.RestartGameHandler += RestartGame;
        EventBroker.ResumeGameHandler += ResumeGame;
        
        InvokeRepeating(nameof(SpawnObstacle), 1.0f, 2.0f);
        InvokeRepeating(nameof(SpawnCoin), 0.5f,0.2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
                ResumeGame();
            else if(!_gameIsPaused)
                Pause();
        }
    }

    private void ResumeGame()
    {
        _pauseMenuScreen.gameObject.SetActive(false);
        _gameIsPaused = false;
        Time.timeScale = 1f;
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        _pauseMenuScreen.gameObject.SetActive(true);
        _gameIsPaused = true;
    }

    // Pooling obstacles from objects pool
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

    private void GameOver()
    {
        _isGameOver = true;
        _gameOverScreen.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        EventBroker.GameOverHandler -= GameOver;
        EventBroker.RestartGameHandler -= RestartGame;
        EventBroker.ResumeGameHandler -= RestartGame;

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);
    }

}
