using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Anger_Bar : MonoBehaviour
{
    public Slider angerBar;  // Reference to the UI Slider component
    public float increaseRate = 1f;  // Rate at which the anger bar increases
    public float maxAnger = 10f;  // Maximum value of the anger bar
    public GameObject gameOverScreen;  // Reference to the game over screen object

    private float currentAnger = 0f;  // Current value of the anger bar

    void Start()
    {
        // Initialize the anger bar value
        angerBar.value = currentAnger;
        angerBar.maxValue = maxAnger;
    }

    void Update()
    {
        // Example condition to increase the anger bar (this can be customized)
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseAnger(increaseRate);
        }

        // Check if the anger bar has reached its maximum value
        if (currentAnger >= maxAnger)
        {
            TriggerGameOver();
        }
    }

    public void IncreaseAnger(float amount)
    {
        currentAnger += amount;
        angerBar.value = currentAnger;
    }

    private void TriggerGameOver()
    {
        // Display the game over screen
        gameOverScreen.SetActive(true);
        
        // Optionally, pause the game
        Time.timeScale = 0f;
    }
}