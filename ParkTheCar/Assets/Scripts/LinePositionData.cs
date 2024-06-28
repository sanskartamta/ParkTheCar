using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLinePositionData", menuName = "LinePositionData")]
public class LinePositionData : ScriptableObject
{
    public List<Vector3> points;
}