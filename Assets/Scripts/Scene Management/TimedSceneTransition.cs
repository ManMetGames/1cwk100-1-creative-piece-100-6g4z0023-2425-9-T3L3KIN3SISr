using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TimedSceneTransition : MonoBehaviour
{
    public Image fadeImage;                // Reference to the UI Image that will be used for the fade effect
    public float fadeDuration = 1f;        // Duration of the fade effect
    public float waitDuration = 3f;        // Duration to wait before transitioning
    public string nextScene;               // Name of the scene to transition to

    private void Start()
    {
        // Ensure the fade image is completely transparent at the start
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0; // Set alpha to 0 (transparent)
            fadeImage.color = color;
        }

        // Start the scene transition
        StartCoroutine(StartSceneTransition());
    }

    private IEnumerator StartSceneTransition()
    {
        // Start fading to black
        yield return StartCoroutine(Fade(1)); // Fade in (to black)

        // Wait for the specified duration
        yield return new WaitForSeconds(waitDuration);

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
}
