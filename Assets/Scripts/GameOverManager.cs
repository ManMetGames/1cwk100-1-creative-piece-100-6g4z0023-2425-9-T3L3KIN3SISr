using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;  // Reference to the Game Over Canvas
    public float restartDelay = 3f;    // Time in seconds before restarting the scene

    private void Start()
    {
        // Ensure the Game Over Canvas is hidden at the start of the game
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    // Method to be called when the collision is detected
    public void TriggerGameOver()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Activate the Game Over Canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Start the restart coroutine
        StartCoroutine(RestartSceneAfterDelay());
    }

    private IEnumerator RestartSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSecondsRealtime(restartDelay); // Use WaitForSecondsRealtime to ignore time scale

        // Reset the time scale to normal before restarting
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
