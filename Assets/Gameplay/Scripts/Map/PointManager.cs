using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private static PointManager instance;
    public static PointManager Instance {
        get{
            if (instance == null)
            {
                instance = FindObjectOfType<PointManager>();
            }
            return instance;
        }
    }
    public PointPath pointPrefabs;
    public StackPath stackPrefabs;
    public GameObject goalPrefabs;
    public PointPath startPoint;
    private PointPath endPoint;
    private Transform tfPointManager;
    private MapSettings curSettings;
    private List<PointPath> pointPath;
    private int curPoint = 0;
    private int level;

    private void OnInit(){
        curPoint = 0;
        level = DataManager.GetLevel();
        // curSettings = settings[Mathf.Clamp(level, 0, settings.Count - 1)];
        pointPath = new List<PointPath>();
        tfPointManager = transform;
        curSettings = Resources.Load<MapSettings>("Levels/Level_" + Mathf.Clamp(level, 0, Resources.LoadAll("Levels/", typeof(MapSettings)).Length - 1).ToString());
    }
    private void Awake()
    {
        OnInit();
        // startPoint = transform.GetChild(0).GetChild(0).GetComponent<PointPath>();
        startPoint.SetNextPoint(curSettings.pointsMap[1].position);
        pointPath.Add(startPoint);
        Vector3 curRotation = Vector3.zero;
        int point = 0;
        
        for (int i = 1; i < curSettings.pointsMap.Count; i++)
        {
            PointPath path = Instantiate(pointPrefabs, tfPointManager.GetChild(0));
            Vector3 dir = curSettings.pointsMap[i].position - curSettings.pointsMap[i - 1].position;
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
            path.transform.localPosition = curSettings.pointsMap[i].position;
            path.transform.eulerAngles = curRotation;
            path.SetPrevPoint(curSettings.pointsMap[i - 1].position);
            point += (curSettings.pointsMap[i].isIncreaseStack ? 1 : -1) * (int)dir.magnitude;
            for (int j = 1; j < Vector3.Distance(curSettings.pointsMap[i].position, curSettings.pointsMap[i - 1].position); j++)
            {
                StackPath stackPath = Instantiate(stackPrefabs, tfPointManager.GetChild(1));
                stackPath.transform.localPosition = curSettings.pointsMap[i - 1].position + j * dir.normalized;
                stackPath.transform.localEulerAngles = curRotation;
                stackPath.SetStatusPath(curSettings.pointsMap[i].isIncreaseStack, !curSettings.pointsMap[i].isIncreaseStack);
            }
            path.SetNextPoint(curSettings.pointsMap[Mathf.Clamp(i + 1,0, curSettings.pointsMap.Count - 1)].position);
            path.GetComponent<StackPath>().SetStatusPath(curSettings.pointsMap[i].isIncreaseStack, !curSettings.pointsMap[i].isIncreaseStack);
            pointPath.Add(path);
        }
        for(int i = 0; i < point; i++)
        {
            StackPath stackPath = Instantiate(stackPrefabs, tfPointManager.GetChild(1));
            stackPath.transform.localPosition = curSettings.pointsMap[curSettings.pointsMap.Count - 1].position + i * Vector3.forward;
            stackPath.transform.localEulerAngles = Vector3.zero;
            stackPath.SetStatusPath(false, true);
        }
        GameObject goalObject = Instantiate(goalPrefabs, tfPointManager);
        goalObject.transform.localPosition = curSettings.pointsMap[curSettings.pointsMap.Count - 1].position + point * Vector3.forward;
        endPoint = goalObject.transform.GetComponentInChildren<PointPath>();
        endPoint.SetNextPoint(endPoint.transform.position);
    }

    public PointPath GetCurPoint(Vector3 direction)
    {
        Vector3 x;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            x = Vector3.right * direction.x;
        }
        else
        {
            x = Vector3.forward * direction.y;
        }
        int next = pointPath[curPoint].CheckDirection(x);
        if (next != 0)
        {
            curPoint = Mathf.Clamp(curPoint + next, 0, pointPath.Count - 1);
            return pointPath[curPoint];
        }
        return null;
    }
    public PointPath GetEndPoint()
    {
        return endPoint;
    }
}
