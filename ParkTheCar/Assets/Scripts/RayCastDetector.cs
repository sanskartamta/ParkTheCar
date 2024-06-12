using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ContactInfo
{
    public bool contacted;
    public Vector3 point;
    public Collider collider;
    public Transform transform;
}
public class RayCastDetector
{
    public ContactInfo RayCast(int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, 1<<layerMask);

        return new ContactInfo
        {
            contacted = hit,
            point = hitInfo.point,
            collider = hitInfo.collider,
            transform = hitInfo.transform

        };
    }
}
