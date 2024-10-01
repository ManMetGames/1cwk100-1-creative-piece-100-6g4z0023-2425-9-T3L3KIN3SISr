using UnityEngine;

public class PauseOnCollision : MonoBehaviour
{
    public GameOverManager gameOverManager; // Reference to the GameOverManager script

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name);

        // Check if the colliding object has the tag "Car" and this object has the tag "Player"
        if (other.CompareTag("Car") && gameObject.CompareTag("Player"))
        {
            if (gameOverManager != null)
            {
                Debug.Log("Game Over Manager Triggered!");
                gameOverManager.TriggerGameOver();
            }
        }
    }
}
