using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The GameObject the camera will follow
    public float smoothSpeed = 0.125f; // Adjusts the smoothness of the camera movement
    public Vector3 offset; // Offset position of the camera relative to the target

    private float initialZ; // Stores the initial Z position of the camera

    void Start()
    {
        // Record the initial Z position of the camera
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position by only following the target's Z position
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z) + offset;
            // Smoothly move the camera to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Lock the X and Y position to maintain the initial values
            transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPosition.z);
        }
    }
}
