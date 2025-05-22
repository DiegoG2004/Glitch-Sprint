using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Swipe Variables")]
    public Transform m_DraggableObject;
    public bool m_Dragging = false;
    public Vector2 m_CurrentTouchPosition;
    public float MinDistanceToChangeDirection = 1.5f;
    [Header("Lane Switching")]
    public int m_CurrentLane = 0; //-1 = left, 0 = middle, 1 = right
    public float m_DistanceBetweenLanes = 2f;
    [Header("Others")]
    public bool m_AdReviveUsed;
    public float m_StarReviveAdRevive;

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
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                    if (hit.collider != null)
                    {
                        m_DraggableObject = hit.collider.transform;
                        m_Dragging = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (m_Dragging)
                    {
                        m_CurrentTouchPosition = touch.position;
                        Vector3 newPos = Camera.main.ScreenToWorldPoint(m_CurrentTouchPosition);
                        newPos.z = 0;
                        if (this.gameObject.transform.position.x + MinDistanceToChangeDirection <= m_DraggableObject.transform.position.x) ChangeLane(1); //Move to the Right
                        else if (this.gameObject.transform.position.x - MinDistanceToChangeDirection >= m_DraggableObject.transform.position.x) ChangeLane(-1); //Move to the Left
                    }
                    break;
                case TouchPhase.Ended:
                    if (m_Dragging)
                    {
                        m_Dragging = false;
                        m_DraggableObject = null;
                    }
                    break;
            }

        }
        //Done with swipes actually
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
