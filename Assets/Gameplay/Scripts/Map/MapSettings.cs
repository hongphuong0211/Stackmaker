using System.Collections.Generic;
using System;
using UnityEngine;
[CreateAssetMenu(menuName = "new level")]
public class MapSettings : ScriptableObject
{
    public List<Point> pointsMap;
    public int coint;
}
[Serializable]
public class Point
{
    public Vector3 position;
    public bool isContinuos;
    public bool isIncreaseStack;
}
