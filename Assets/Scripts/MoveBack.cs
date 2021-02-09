using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private float _backBound = -5f;
    private float _speed = 20f;

    private void Update()
    {
        if (!GameManager.IsGameOver)
        {
            transform.Translate(Vector3.back * (Time.deltaTime * _speed));
            
            if(transform.position.z < _backBound)
                gameObject.SetActive(false);
        }
    }
}
