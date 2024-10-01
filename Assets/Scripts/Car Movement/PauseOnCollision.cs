using UnityEngine;

public class PauseOnCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // Reference to the GameOverManager script

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name); // Log the name of the colliding object

        // Check if the colliding object has the tag "Car" and this object has the tag "Player"
        if (other.CompareTag("Car") && gameObject.CompareTag("Player"))
        {
            // Trigger the Game Over state using the GameOverManager
            if (gameOverManager != null)
            {
                Debug.Log("Game Over Manager Triggered!");
                gameOverManager.TriggerGameOver();
            }
            else
            {
                Debug.LogWarning("GameOverManager reference is missing on " + gameObject.name);
            }
        }
    }
}
