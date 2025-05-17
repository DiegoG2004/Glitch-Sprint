using Unity.VisualScripting;
using UnityEngine;

public class DragInputExample : MonoBehaviour
{
    public Transform m_DraggableObject;
    public bool m_IsDragging = false;
    public Vector3 m_CurrentTouchPosition;

    // Update is called once per frame
    void Update()
    {
        //If there's at least 1 touch
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
                        m_IsDragging = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (m_IsDragging)
                    {
                        m_CurrentTouchPosition = touch.position;
                        Vector3 newPos = Camera.main.ScreenToWorldPoint(m_CurrentTouchPosition);
                        newPos.z = 0;
                        m_DraggableObject.position = newPos;
                    }
                    break;
                case TouchPhase.Ended:
                    if (m_IsDragging)
                    {
                        m_IsDragging = false;
                        m_DraggableObject = null;
                    }
                    break;
            }
        }
    }
}
