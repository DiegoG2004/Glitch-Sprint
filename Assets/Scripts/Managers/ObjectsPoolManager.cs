using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolManager : MonoBehaviour
{
    public static ObjectsPoolManager m_Instance;
    [Header("Prefabs")]
    public GameObject m_ObstaclePrefab;
    public GameObject m_CoinPrefab;
    //Queues are pools where items are taken in order.
    //Works when the pool gets filled by a bunch of the same object
    public Queue<GameObject> m_ObstaclePool = new Queue<GameObject>();
    public Queue<GameObject> m_CoinPool = new Queue<GameObject>();
    [Header("Pool sizes")]
    public int m_ObstaclePoolSize = 10;
    public int m_CoinPoolSize = 30;

    private void Awake()
    {
        //Instance of the class
        m_Instance = this;
        //Initialize the pools
        PopulatePool(m_ObstaclePrefab, m_ObstaclePool, m_ObstaclePoolSize);
        PopulatePool(m_CoinPrefab, m_CoinPool, m_CoinPoolSize);
    }

    public void PopulatePool(GameObject thisPrefab, Queue<GameObject> thisQueue, int thisAmount)
    {
        for (int i = 0; i < thisAmount; i++)
        {
            GameObject newObject = GameObject.Instantiate(thisPrefab);
            newObject.SetActive(false);
            thisQueue.Enqueue(newObject);
        }
    }

    public GameObject GetObstacle()
    {
        return GetFromPool(m_ObstaclePrefab, m_ObstaclePool);
    }    
    public GameObject GetCoin()
    {
        return GetFromPool(m_CoinPrefab, m_CoinPool);
    }

    public void ReturnObstacle(GameObject thisObstacle)
    {
        thisObstacle.SetActive(false);
        thisObstacle.transform.parent = null;
        m_ObstaclePool.Enqueue(thisObstacle);
    }
    public void ReturnCoin(GameObject thisCoin)
    {
        thisCoin.SetActive(false);
        thisCoin.transform.parent = null;
        m_CoinPool.Enqueue(thisCoin);
    }

    private GameObject GetFromPool(GameObject thisPrefab, Queue<GameObject> thisQueue)
    {
        if (thisQueue.Count ==0)
        {
            //Add a new object to the queue
            GameObject newObject = GameObject.Instantiate(thisPrefab);
            newObject.SetActive(false);
            thisQueue.Enqueue(newObject);
        }
        GameObject objectToReturn = thisQueue.Dequeue();
        objectToReturn.SetActive(true);
        return objectToReturn;
    }
}
