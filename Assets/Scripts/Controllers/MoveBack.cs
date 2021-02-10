using UnityEngine;

//this class moves obstacles and coins on player creating an illusion of movement
namespace Controllers
{
    public class MoveBack : MonoBehaviour
    {
        private float _backBound = -5f;
        private float _speed = 15f;
        private bool _isGameOver;

        private void Start()
        {
            //This event fires when game is over
            EventBroker.StopMovingObjectsHandler += StopMoving;
            EventBroker.UpdateSpeedHandler += UpdateSpeed;
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                transform.Translate(Vector3.back * (Time.deltaTime * _speed));
            
                if(transform.position.z < _backBound)
                    gameObject.SetActive(false);
            }
        }

        void StopMoving()
        {
            _isGameOver = true;
            EventBroker.StopMovingObjectsHandler -= StopMoving;
            EventBroker.UpdateSpeedHandler -= UpdateSpeed;
        }

        //Updates speed by value
        void UpdateSpeed(float addToSpeed)
        {
            _speed += addToSpeed;
        }
    }
}
