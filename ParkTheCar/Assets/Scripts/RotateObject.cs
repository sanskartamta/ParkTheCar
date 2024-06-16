using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int Rotatespeed = 1;


    void Update()
    {
        transform.Rotate(0, Rotatespeed, 0, Space.World);
    }
}
