using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

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
        InvokeRepeating(nameof(SpawnCoin), 0.5f,0.2f);
    }

    // Pooling obstacles from objects pool
    void SpawnObstacle()
    {
        GameObject pooledObstacle = ObjectPooler.Instance.GetPooledObstacle();
        if (pooledObstacle != null)
        {
            pooledObstacle.SetActive(true);
            pooledObstacle.transform.position = GetSpawnPosition();
        }
    }

    void SpawnCoin()
    {
        GameObject pooledCoin = ObjectPooler.Instance.GetPooledCoin();
        if (pooledCoin != null)
        {
            pooledCoin.SetActive(true);
            pooledCoin.transform.position = new Vector3(GetSpawnPosition().x, 1f, GetSpawnPosition().z);
        }
    }

    Vector3 GetSpawnPosition()
    {
        Random rnd = new Random();
        int sp = rnd.Next(xSpawnPositions.Length);
        Vector3 spawnPosition = xSpawnPositions[sp];
        return spawnPosition;
    }
}
