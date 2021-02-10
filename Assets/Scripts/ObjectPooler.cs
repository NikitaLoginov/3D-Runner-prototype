using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    //Obstacles
    public List<GameObject> pooledObstacles;
    public GameObject obstacleToPool;
    public int amountOfObstaclesToPool;

    //Coins
    public List<GameObject> pooledCoins;
    public GameObject coinToPool;
    public int amountOfCoinsToPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Something tried to spawn another ObjectPooler");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pooledObstacles = new List<GameObject>();
        pooledCoins = new List<GameObject>();
        
        for (int i = 0; i < amountOfObstaclesToPool; i++)
        {
            GameObject obst = Instantiate(obstacleToPool, this.transform, true); // instantiating obstacle as child of "ObjectPooler" object
            obst.SetActive(false);
            pooledObstacles.Add(obst);
        }

        for (int i = 0; i < amountOfCoinsToPool; i++)
        {
            GameObject coin = Instantiate(coinToPool, this.transform, true);
            coin.SetActive(false);
            pooledCoins.Add(coin);
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

    public GameObject GetPooledCoin()
    {
        for (int i = 0; i < pooledCoins.Count; i++)
        {
            if (!pooledCoins[i].activeInHierarchy)
                return pooledCoins[i];
        }
        return null;
    }
}
