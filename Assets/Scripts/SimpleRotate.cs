using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // degrees per second

    [Header("Active Axes")]
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    void Update()
    {
        Vector3 rotation = new Vector3(
            rotateX ? 1f : 10f,
            rotateY ? 1f : 0f,
            rotateZ ? 1f : 0f
        );

        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
