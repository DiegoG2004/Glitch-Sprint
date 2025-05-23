using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EndlessRunnerTileManager : MonoBehaviour
{
    [Header("Tile Options")]
    public GameObject[] m_TilePrefabs;
    public float m_TileLength = 44f;
    public int m_IndexToSpawn = 5;
    public int m_MaxInstancesPerTile = 3;
    private Vector3 m_TileSpawnOffset = new Vector3(-57.4f, 5.2f, 0f); // Ajuste visual

    [Header("Movement Options")]
    public float m_MaxSpeed = 5f;
    public float m_SlowSpeed = 2f;
    private float m_CurrentSpeed;
    public float m_TimeToRecover = 5f;

    [Header("Tile Pools")]
    public List<GameObject> m_ActiveTiles = new List<GameObject>();
    public List<GameObject> m_TilePool = new List<GameObject>();

   

    private void PlaceRandomTile(int positionindex = 0)
    {
        if (m_TilePool.Count == 0) return;

        int index = Random.Range(0, m_TilePool.Count);
        GameObject tile = m_TilePool[index];
        m_TilePool.RemoveAt(index);

        float zpos = positionindex * m_TileLength;
        tile.transform.position = m_TileSpawnOffset + new Vector3(0, 0, zpos); // APLICAR OFFSET

        m_ActiveTiles.Add(tile);
        tile.SetActive(true);
    }


    void Awake()
    {
        m_CurrentSpeed = m_MaxSpeed;
        InitializePool();
        PlaceInitialTiles();
    }

    private void InitializePool()
    {
        foreach (var tile in m_TilePrefabs)
        {
            for (int i = 0; i < m_MaxInstancesPerTile; i++)
            {
                GameObject newTile = GameObject.Instantiate(tile);
                newTile.SetActive(false);
                m_TilePool.Add(newTile);
            }
        }
    }
    private void PlaceInitialTiles()
    {
        for (int i = 0; i < m_IndexToSpawn; i++)
        {
            //Place tiles on the origin, 1 in front and 2 in front
            PlaceRandomTile(i);
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        MoveActiveTiles();
    }
    void MoveActiveTiles()
    {
        for (int i = 0; i < m_ActiveTiles.Count; i++)
        {
            Transform thisTile = m_ActiveTiles[i].transform;
            thisTile.position += Vector3.back * m_CurrentSpeed * Time.deltaTime;
            if (thisTile.position.z <= -m_TileLength)
            {
                thisTile.gameObject.SetActive(false);
                m_ActiveTiles.RemoveAt(i);
                i--;
                m_TilePool.Add(thisTile.gameObject);
                PlaceRandomTile(m_IndexToSpawn - 1); //Spawn a new tile
            }
        }
    }
    public void ObstacleHit()
    {
        if (m_CurrentSpeed == m_SlowSpeed)
        {
            m_CurrentSpeed = 0;            
            CancelInvoke("RecoverSpeed");
            Debug.Log("Died :<");
        }
        else
        {
            m_CurrentSpeed = m_SlowSpeed;
            Invoke("RecoverSpeed", m_TimeToRecover);
        }
    }
    public void RecoverSpeed()
    {
        m_CurrentSpeed = m_MaxSpeed;
        Debug.Log("Nyooming");
    }
}
