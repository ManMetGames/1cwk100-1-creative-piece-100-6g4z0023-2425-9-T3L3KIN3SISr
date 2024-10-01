using UnityEngine;

public class ContinuousCameraMovement : MonoBehaviour
{
    public float baseSpeed = 5f; // Default speed of movement on the Z-axis
    public float speedMultiplier = 2f; // Multiplier for speed when a key is pressed

    private float currentSpeed; // To track the current movement speed

    void Start()
    {
        currentSpeed = baseSpeed; // Start with the base speed
    }

    void Update()
    {
        // Check if any key is pressed to increase speed
        if (Input.anyKey)
        {
            currentSpeed = baseSpeed * speedMultiplier;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        // Move the object continuously along the Z-axis with the current speed
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
    }
}
