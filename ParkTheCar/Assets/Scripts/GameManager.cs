using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // List of level prefabs to be assigned in the inspector
    public List<GameObject> levelPrefabs;

    // Index of the current level
    private int currentLevelIndex = 0;

    // Reference to the current level instance
    private GameObject currentLevel;

    void Start()
    {
        // Load the first level at the start
        LoadLevel(currentLevelIndex);
    }

    // Method to load a level by index
    public void LoadLevel(int levelIndex)
    {
        // Check if the level index is within bounds
        if (levelIndex >= 0 && levelIndex < levelPrefabs.Count)
        {
            // If there is a current level, destroy it
            if (currentLevel != null)
            {
                Destroy(currentLevel);
            }

            // Instantiate the new level and set it as the current level
            currentLevel = Instantiate(levelPrefabs[levelIndex]);
            currentLevelIndex = levelIndex;
        }
        else
        {
            Debug.LogError("Level index out of bounds!");
        }
    }

    // Method to load the next level
    public void LoadNextLevel()
    {
        // Calculate the next level index
        int nextLevelIndex = currentLevelIndex + 1;

        // Check if the next level index is within bounds
        if (nextLevelIndex < levelPrefabs.Count)
        {
            LoadLevel(nextLevelIndex);
        }
        else
        {
            Debug.Log("All levels completed!");
            // Optionally, you can implement logic here to restart from the first level or end the game
        }
    }

    // Method to restart the current level
    public void RestartCurrentLevel()
    {
        LoadLevel(currentLevelIndex);
    }
}
