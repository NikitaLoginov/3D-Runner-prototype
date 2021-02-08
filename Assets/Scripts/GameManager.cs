using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    private float _spawnRate;

    private Vector3[] xSpawnPositions;

    private void Awake()
    {
        xSpawnPositions = new[] {new Vector3(0, 0, 40), new Vector3(3, 0, 40), new Vector3(-3, 0, 40)};
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1.0f, 2.0f);
    }

    void SpawnObstacle()
    {
        Random rnd = new Random();
        Vector3 spawnPos;
        while (true)
        {
            int xSpawnPos = rnd.Next(xSpawnPositions.Length);
            spawnPos = xSpawnPositions[xSpawnPos];

            GameObject pooledObstacle = ObjectPooler.Instance.GetPooledObstacle();
            if (pooledObstacle != null)
            {
                pooledObstacle.SetActive(true);
                pooledObstacle.transform.position = spawnPos;
            }
            break;
        }
    }
}
