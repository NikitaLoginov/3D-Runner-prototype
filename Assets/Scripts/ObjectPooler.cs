using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<GameObject> pooledObstacles;
    public GameObject obstacleToPool;
    public int amountOfObstaclesToPool;

    private GameObject _obstaclesObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Something tried to spawn a another ObjectPooler");
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        
        pooledObstacles = new List<GameObject>();
        for (int i = 0; i < amountOfObstaclesToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(obstacleToPool, this.transform, true); // instantiating obstacle as child of "ObjectPooler" object
            obj.SetActive(false);
            pooledObstacles.Add(obj);
        }
    }

    public GameObject GetPooledObstacle()
    {
        for (int i = 0; i < pooledObstacles.Count; i++)
        {
            // if obstacle IS NOT active on scene - return that obstacle
            if (!pooledObstacles[i].activeInHierarchy)
                return pooledObstacles[i];

        }
        //if obstacle IS active on scene - return null
        return null;
    }
}
