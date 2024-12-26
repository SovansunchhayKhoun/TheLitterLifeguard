using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f; // Time in seconds
    private float timeRemaining;
    public TextMeshProUGUI timerText; // Reference to the UI Text component
    public TextMeshProUGUI titleText; // Reference to the UI Text component
    public TextMeshProUGUI scoreText; // Reference to the UI Text component
    public UIManager uiManager; // Reference to the UIManager script
    private bool isGameOver = false;

    void Start()
    {
        timeRemaining = timeLimit; // Set the initial time
        uiManager.gameOverPanel.SetActive(false); // Hide the Game Over panel initially
        // uiManager.victoryPanel.SetActive(false); // Hide the Victory panel initially
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            if (isGameOver) return;

            scoreText.text = uiManager.points.ToString();
            uiManager.gameOverPanel.SetActive(true);
            if (uiManager.points > 0)
            {
                Debug.Log("You Win");
                titleText.text = "You Win";
            }
            else
            {
                Debug.Log("Game Over");
                titleText.text = "Game Over";
            }
            isGameOver = true;
            // EndGame(); // Trigger end game when time is up
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

