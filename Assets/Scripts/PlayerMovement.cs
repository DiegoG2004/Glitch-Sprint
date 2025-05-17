using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool m_FakeHit = false;
    [Header("Lane Switching")]
    public int m_CurrentLane = 0; //-1 = left, 0 = middle, 1 = right
    public float m_DistanceBetweenLanes = 2f;

    private EndlessRunnerTileManager m_RunnerTileManager;

    private void Awake()
    {
        m_RunnerTileManager = FindFirstObjectByType<EndlessRunnerTileManager>();
    }    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FakeHit)
        {
            m_FakeHit = false;
            m_RunnerTileManager.ObstacleHit();
        }
        //Do it with swipes actually
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1); //Move to the right
            
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1); //Move to the left
        }
    }

    private void ChangeLane(int direction)
    {
        if (m_CurrentLane == direction) return; //move -1 on lane -1 or move 1 on lane 1 = nuh uh

        m_CurrentLane += direction;
        transform.position = new Vector3(
            m_CurrentLane * m_DistanceBetweenLanes,
            transform.position.y,
            transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.enabled = false;
            m_RunnerTileManager.ObstacleHit();
        }
    }
}
