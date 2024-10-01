using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;                // Reference to the UI Image that will be used for the fade effect
    public float fadeDuration = 1f;        // Duration of the fade effect
    public string nextScene;               // Name of the scene to transition to
    public Camera selectedCamera;           // Reference to the selectable Camera
    public float cameraMoveDistance = 5f;   // Distance to move the camera forward
    public float cameraMoveDuration = 1f;   // Duration of the camera move
    public MonoBehaviour scriptToDisable;   // Script to disable during the fade-out

    private void Start()
    {
        // Ensure the fade image is completely transparent at the start
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0; // Set alpha to 0 (transparent)
            fadeImage.color = color;
        }

        // Ensure that selectedCamera is assigned, if not, try to find the main camera
        if (selectedCamera == null)
        {
            selectedCamera = Camera.main;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is tagged as "Player"
        {
            StartCoroutine(FadeAndTransition());
        }
    }

    private IEnumerator FadeAndTransition()
    {
        // Disable the specified script
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false; // Disable the script
        }

        // Start fading to black and move the camera forward
        yield return StartCoroutine(Fade(1)); // Fade in (to black)
        yield return StartCoroutine(MoveCamera()); // Move camera forward

        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Color color = fadeImage.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the final alpha value is set
        color.a = targetAlpha;
        fadeImage.color = color;
    }

    private IEnumerator MoveCamera()
    {
        if (selectedCamera == null)
        {
            Debug.LogWarning("Selected camera is not assigned. Cannot move the camera.");
            yield break; // Exit if no camera is assigned
        }

        Vector3 startPosition = selectedCamera.transform.position; // Starting position
        Vector3 targetPosition = startPosition + new Vector3(0, 0, cameraMoveDistance); // Target position

        float time = 0;

        while (time < cameraMoveDuration)
        {
            time += Time.deltaTime;
            selectedCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / cameraMoveDuration);
            yield return null;
        }

        // Ensure the camera reaches the final position
        selectedCamera.transform.position = targetPosition;
    }
}
