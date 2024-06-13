using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Experimental.Rendering;

public class Game : MonoBehaviour
{
    // Singleton class:
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();

    private int totalRoutes;
    private int successfulParks;

    // events:
    public UnityAction<Route> OnCarEntersPark;
    public UnityAction OnCCollision;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        successfulParks = 0;

        OnCarEntersPark += OnCarEntersParkHandler;
        OnCCollision += OnCCollisionHandler;
    }

    private void OnCCollisionHandler()
    {
        Debug.Log("GAME OVER");

        
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

        if (successfulParks == totalRoutes)
        {
            Debug.Log("You Win!!");
                int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevel < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(nextLevel);
                else
                    Debug.LogWarning("NO NEXT LEVEL AVAILABLE");
            });
        }
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        if (readyRoutes.Count == totalRoutes)
            MoveAllCars();
    }

    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
            route.car.Move( route.linePoints); 
    }
}
