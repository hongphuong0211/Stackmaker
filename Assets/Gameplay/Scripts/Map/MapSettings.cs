using System.Collections.Generic;
using System;
using UnityEngine;
// [CreateAssetMenu(menuName = "new level")]
public class MapSettings: MonoBehaviour
{
    public PointPath pointPrefabs;
    public StackPath stackPrefabs;

    public List<Point> pointsMap;
    public int coint;

    private Transform tfSettings;

    public Transform TFSettings{
        get{ 
            if(tfSettings == null){
                tfSettings = transform;
            }
            return tfSettings;
        }
    }


    public void BuildPath(){
        Reset();
        List<PointPath> pointPath = new List<PointPath>();
        Vector3 curRotation = Vector3.zero;
        int point = 0;
        PointPath startPoint = Instantiate<PointPath>(pointPrefabs, tfSettings);
        startPoint.transform.position = pointsMap[0].position;
        startPoint.SetNextPoint(pointsMap[1].position);
        pointPath.Add(startPoint);
        for (int i = 1; i < pointsMap.Count; i++)
        {
            PointPath path = Instantiate(pointPrefabs, tfSettings);
            Transform tfPath = path.transform;
            Vector3 dir = pointsMap[i].position - pointsMap[i - 1].position;
            if (dir.x == 0)
            {
                curRotation = Vector3.zero;
            }else if (dir.x < 0)
            {
                curRotation = Vector3.up * (-90);
            }else
            {
                curRotation = Vector3.up * 90;
            }
            tfPath.localPosition = pointsMap[i].position;
            tfPath.eulerAngles = curRotation;
            path.SetPrevPoint(pointsMap[i - 1].position);
            point += (pointsMap[i].isIncreaseStack ? 1 : -1) * (int)dir.magnitude;
            for (int j = 1; j < Vector3.Distance(pointsMap[i].position, pointsMap[i - 1].position); j++)
            {
                StackPath stackPath = Instantiate(stackPrefabs, tfSettings);
                stackPath.transform.localPosition = pointsMap[i - 1].position + j * dir.normalized;
                stackPath.transform.localEulerAngles = curRotation;
                stackPath.SetStatusPath(pointsMap[i].isIncreaseStack, !pointsMap[i].isIncreaseStack);
            }
            path.SetNextPoint(pointsMap[Mathf.Clamp(i + 1,0, pointsMap.Count - 1)].position);
            path.GetComponent<StackPath>().SetStatusPath(pointsMap[i].isIncreaseStack, !pointsMap[i].isIncreaseStack);
            pointPath.Add(path);
        }
        for(int i = 0; i < point; i++)
        {
            StackPath stackPath = Instantiate(stackPrefabs, tfSettings);
            Transform tfStack = stackPath.transform;
            tfStack.localPosition = pointsMap[pointsMap.Count - 1].position + i * Vector3.forward;
            tfStack.localEulerAngles = Vector3.zero;
            stackPath.SetStatusPath(false, true);
        }
    }

    public void Reset(){
        for(int i = TFSettings.childCount - 1; i > -1; i--){
            DestroyImmediate(TFSettings.GetChild(i).gameObject);
        }
    }
}
[Serializable]
public class Point
{
    public Vector3 position;
    public bool isContinuos;
    public bool isIncreaseStack;
}
