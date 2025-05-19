using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileManager : MonoBehaviour
{
    public float m_TileLength = 18f;
    [Header("Obstacles and Coins")]
    public int m_ObstacleCount = 3;
    public int m_CoinCount = 8;
    public Transform m_ObstaclesRoot;
    public Transform m_CoinsRoot;

    [Header("Lanes")]
    public float[] m_LanePositions = new float[] { -2f, 0f, 2f };
    //OnEnable triggers when the object is active and this script is enabled
    private void OnEnable()
    {
        if (m_ObstaclesRoot == null)
        {
            //Create the root if this tile doesn't have one
            GameObject newRoot = new GameObject("ObstaclesRoot");
            newRoot.transform.parent = transform;
            newRoot.transform.localPosition = Vector3.zero;
            m_ObstaclesRoot = newRoot.transform;
        }
        if (m_CoinsRoot == null)
        {
            GameObject newRoot = new GameObject("CoinsRoot");
            newRoot.transform.parent = transform;
            newRoot.transform.localPosition = Vector3.zero;
            m_CoinsRoot = newRoot.transform;
        }
        SpawnCoins();
        SpawnObstacles();
    }
    private void OnDisable()
    {
        for (int i = m_ObstaclesRoot.childCount -1; i >=0 ; i--)
        {
            GameObject obstacle = m_ObstaclesRoot.GetChild(i).gameObject;
            ObjectsPoolManager.m_Instance.ReturnObstacle(obstacle);
        }
        for (int i = m_CoinsRoot.childCount - 1; i >= 0; i--)
        {
            GameObject coin = m_CoinsRoot.GetChild(i).gameObject;
            ObjectsPoolManager.m_Instance.ReturnCoin(coin);
        }
    }

    private void SpawnCoins()
    {
        float lengthBetweenCoins = 1f;
        int laneIndex = Random.Range(0, m_LanePositions.Length);
        for (int i = 0; i < m_CoinCount; i++)
        {
            //Where the coins spawn
            float zPos = transform.position.z + lengthBetweenCoins * (i + 1);
            float xPos = m_LanePositions[laneIndex];
            //Take a coin from the ObjectsPoolManager
            GameObject newCoin = ObjectsPoolManager.m_Instance.GetCoin();
            newCoin.transform.position = new Vector3(xPos, 0, zPos);
            newCoin.transform.parent = m_CoinsRoot;
        }
    }

    private void SpawnObstacles()
    {
        //A random number of obstacles in this tile
        int ObstacleLanesToSpawn = Random.Range(1, m_ObstacleCount + 1);

        float LengthBetweenObstacles = m_TileLength / (ObstacleLanesToSpawn + 1);

        for (int i = 0; i < ObstacleLanesToSpawn; i++)
        {
            //Variables to decide where to spawn the obstacle (on the appropiate tile)
            float zPos = transform.position.z + LengthBetweenObstacles * (i + 1);
            int obstaclesPerLine = Random.Range(1, 3);
            
            //To make sure obstacles don't spawn on the same lane twice
            //...very cursed but i don't wanna find a better way lol
            List<int> lanesToUse = new List<int>();
            while (lanesToUse.Count < obstaclesPerLine)
            {
                int newLaneIndex = Random.Range(0, m_LanePositions.Length);
                if (!lanesToUse.Contains(newLaneIndex))
                {
                    lanesToUse.Add(newLaneIndex);
                }
            }
            //Place the obstacles
            for (int j = 0; j < obstaclesPerLine; j++)
            {
                int laneIndex = lanesToUse[j];
                float xPos = m_LanePositions[laneIndex];
                //Take an obstacle from the ObjectsPoolManager
                GameObject newObstacle = ObjectsPoolManager.m_Instance.GetObstacle();
                newObstacle.transform.position = new Vector3(xPos, 0, zPos);
                newObstacle.transform.parent = m_ObstaclesRoot;
            }
        }
    }


}
