using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointPath : MonoBehaviour
{
    public bool isContinuos = false;
    private Vector3 prevPoint = Vector3.zero;
    private Vector3 nextPoint = Vector3.zero;

    public Vector3 NextPoint { get { return nextPoint; } }
    public void SetPrevPoint(Vector3 newPos)
    {
        prevPoint = newPos;
    }

    public void SetNextPoint(Vector3 newPos)
    {
        nextPoint = newPos;
    }

    public int CheckDirection(Vector3 dirMove)
    {
        if((dirMove.normalized - (prevPoint - transform.position).normalized).sqrMagnitude == 0)
        {
            return -1;
        }
        if ((dirMove.normalized - (nextPoint - transform.position).normalized).sqrMagnitude == 0)
        {
            return 1;
        }
        return 0;
    }
}
