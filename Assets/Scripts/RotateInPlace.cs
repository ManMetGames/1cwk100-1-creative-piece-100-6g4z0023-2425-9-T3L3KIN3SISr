using UnityEngine;

public class RotateInPlace : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = new Vector3(0f, 50f, 0f); // Rotation speed for each axis (degrees per second)

    void Update()
    {
        // Apply the rotation to the GameObject
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
