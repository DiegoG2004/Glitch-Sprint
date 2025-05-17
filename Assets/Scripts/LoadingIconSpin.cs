using UnityEngine;

public class LoadingIconSpin : MonoBehaviour
{
    public bool RotateX;
    public bool RotateY;
    public bool RotateZ;

    public float m_RotationSpeed;
    void Update()
    {
        Vector3 rotation = Vector3.zero;

        if (RotateX)
        { 
        rotation.x = 1f; 
        }
        if (RotateY)
        {
            rotation.y = 1f;
        }
        if (RotateZ)
        {
            rotation.z = 1f;
        }
        transform.Rotate(rotation * m_RotationSpeed * Time.deltaTime);
    }
}
