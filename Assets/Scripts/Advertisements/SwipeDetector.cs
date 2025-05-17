using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 m_StartPosition;
    private bool m_IsSwiping;
    public float m_SwipeThreshold = 50f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch);

        }
    }
    //The actual function to deal with swiping
    void HandleTouch(Touch thisTouch)
    {
        if (thisTouch.phase == TouchPhase.Began && !m_IsSwiping)
        {
            m_StartPosition = thisTouch.position;
            m_IsSwiping = true;
        }
        else if(thisTouch.phase == TouchPhase.Ended && m_IsSwiping)
        {
            Vector2 endPosition = thisTouch.position;
            Vector2 swipeDelta = endPosition - m_StartPosition;
            swipeDelta.y = 0; //swipe horizontal
            if (swipeDelta.magnitude > m_SwipeThreshold) 
            {
                Debug.Log(swipeDelta.magnitude);
                if (endPosition.x < m_StartPosition.x)
                {
                    Debug.Log("Swiped left");
                }
                else
                {
                    Debug.Log("Swiped right");
                }
            }
            m_IsSwiping = false;
        }
    }
}
