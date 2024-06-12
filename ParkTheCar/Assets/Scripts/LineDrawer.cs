using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] UserInput userInput;

    [SerializeField] int interactableLayer;

    private Line currentLine;
    private Route currentRoute;

    RayCastDetector raycastDetector = new();

    private void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandler;
        userInput.OnMouseMove += OnMouseMoveHandler;
        userInput.OnMouseUp += OnMouseUpHandler;
    }

    private void OnMouseDownHandler()
    {
        ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

        if (contactInfo.contacted)
        {
            bool isCar = contactInfo.collider.TryGetComponent(out Car _car);
            
            if (isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();
            }
        }
    }

    private void OnMouseMoveHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                Vector3 newPoint = contactInfo.point;
                currentLine.AddPoint(newPoint);

                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (isPark)
                {
                    Route parkRoute = _park.route;
                    if (parkRoute == currentRoute)
                    {
                        currentLine.AddPoint(contactInfo.transform.position);
                    }

                    else
                    {
                        currentLine.Clear();
                    }

                    OnMouseUpHandler();
                }
            }
        }
    }

    private void OnMouseUpHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (currentLine.pointsCount < 2 || !isPark)
                {
                    currentLine.Clear();
                }
                else
                {
                    currentRoute.Disactivate();
                }
            }
            else
            {
                currentLine.Clear();
            }
        }
        ResetDrawer();
    }   
    private void ResetDrawer()
    {
        currentRoute = null;
        currentLine = null;



     }
}
