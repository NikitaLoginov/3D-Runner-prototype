using System;
using UnityEngine;

//This class is used from dramatic pan-out on death
namespace Controllers
{
    public class MoveCamera : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _onDeathPositionZ;
        private float _speed = 5f;
        private bool _isGameOver;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _onDeathPositionZ = -8.5f;
        }

        private void Start()
        {
            EventBroker.GameOverHandler += GameOver;
        }

        private void Update()
        {
            if(_isGameOver)
                PanOutOnDeath();
        }

        private void GameOver()
        {
            _isGameOver = true;
            EventBroker.GameOverHandler -= GameOver;
        }

        private void PanOutOnDeath()
        {
            if (Math.Abs(_mainCamera.transform.position.z - _onDeathPositionZ) > 0.01f)
                _mainCamera.transform.Translate(0,0,-Time.deltaTime);

        }
    }
}
