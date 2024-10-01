using UnityEngine;

public class MoveWithOppositeSpeed : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // Speed of movement along the X-axis
    [SerializeField] private float duration = 5f; // Total time for the object to reset (5 seconds)
    [SerializeField] private float distance = 10f; // Maximum distance to move on the X-axis
    [SerializeField] private float delay = 2f; // Delay before the script starts functioning

    private float movementTimer = 0f; // Timer to track the movement
    private float delayTimer = 0f; // Timer to track the initial delay
    private Vector3 startPosition; // Initial position of the object

    private bool isDelayCompleted = false; // Tracks if the delay has been completed

    void Start()
    {
        // Save the initial position
        startPosition = transform.position;
    }

    void Update()
    {
        // Track the delay time until it's completed
        if (!isDelayCompleted)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delay)
            {
                isDelayCompleted = true; // Mark the delay as completed
            }
            return; // Exit Update until the delay is complete
        }

        // Increment the movement timer by the time passed since the last frame
        movementTimer += Time.deltaTime;
        soundTimer += Time.deltaTime; // Track the time for sound

        // Calculate the movement based on speed and time, then negate it to reverse direction
        float xMovement = -Mathf.PingPong(movementTimer * speed, distance); // Negate the movement

        // Update the GameObject's position along the X-axis in the opposite direction
        transform.position = startPosition + new Vector3(xMovement, 0f, 0f);

        // Reset the movement timer and position if the duration is exceeded
        if (movementTimer >= duration)
        {
            movementTimer = 0f; // Reset movement timer
            transform.position = startPosition; // Reset position
        }
    }
}
