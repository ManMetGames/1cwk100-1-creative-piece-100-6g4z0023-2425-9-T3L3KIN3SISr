using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    [SerializeField] public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    void UpdateTimer(float value)
    {
        timeRemaining += value;
        timerText.text = "Timer: " + timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                UpdateTimer(-1*Time.deltaTime);
            }
            else
            {
                Debug.Log("Countdown has finished. Starting Game!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
