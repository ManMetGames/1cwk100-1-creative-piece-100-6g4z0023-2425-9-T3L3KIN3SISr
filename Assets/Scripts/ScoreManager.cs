using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TMP_Text for score display
    public TMP_Text finalScoreText; // Reference to the TMP_Text for final score display after hitting "Final"
    public float scoreIncreaseAmount = 10f; // The amount of score to increase per key input
    public GameObject player; // Reference to the player game object

    private float score = 0f; // The current score
    private bool isPaused = false; // To check if the game is paused

    private void Start()
    {
        // Initialize score text display
        UpdateScoreText();
        finalScoreText.text = "";
    }

    private void Update()
    {
        
        if (Input.anyKey && !isPaused)
        {
            score += scoreIncreaseAmount;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Final"))
        {
            finalScoreText.text = "Final Score: " + score;
            Time.timeScale = 0f; // Stop the game
            Debug.Log("Final Score: " + score);
        }
    }
}
