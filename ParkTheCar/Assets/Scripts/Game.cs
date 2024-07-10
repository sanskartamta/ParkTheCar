using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class Game : MonoBehaviour
{
    // Singleton instance
    public static Game Instance;

    // UI button reference
    

    // List of routes
    [HideInInspector] public List<Route> readyRoutes = new List<Route>();

    // Total number of routes and successful parks
    private int totalRoutes;
    private int successfulParks;


    // Events
    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCCollision;
    public Button nextLevelButton;
    public Button reloadLevelButton;
    public GameObject CompleteLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        nextLevelButton.gameObject.SetActive(false);
        reloadLevelButton.gameObject.SetActive(true);
        CompleteLevel.SetActive(false);

        totalRoutes = FindObjectsOfType<Route>().Length; // Update to find all Routes in the scene
        successfulParks = 0;

        // Subscribe to events
        OnCarEntersPark += OnCarEntersParkHandler;
        OnCCollision += OnCCollisionHandler;


    }

    private void OnCCollisionHandler()
    {
        Debug.Log("GAME OVER");

        // Restart the current scene after delay
        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel);
        });
    }

    private void OnCarEntersParkHandler(Route route)
    {
        route.car.StopDancingAnim();
        successfulParks++;

        // Check if all parks are successful
        if (successfulParks == totalRoutes)
        {
            Debug.Log("You Win!!");
            nextLevelButton.gameObject.SetActive(true);
            reloadLevelButton.gameObject.SetActive(false);
            CompleteLevel.SetActive(true);

            // Load the next level after a delay
            /*int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextLevel);
                else
                    Debug.LogWarning("NO NEXT LEVEL AVAILABLE");
            });*/
        }
    }

    public void LoadNextScene()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    // Method to register a route
    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        // If all routes are registered, move all cars
        if (readyRoutes.Count == totalRoutes)
            MoveAllCars();
    }

    // Method to move all registered cars
    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
        {
            if (route.car != null)
                route.car.Move(route.linePoints);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
