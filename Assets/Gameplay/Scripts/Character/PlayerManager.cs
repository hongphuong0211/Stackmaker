using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { 
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }
            return instance; } 
    }

    public float speed = 5.0f;
    private bool touchStart = false;
    private bool startGame = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private bool isMoving = false;
    private bool isEnd = false;
    private Vector2 offset;
    private Vector2 direction;
    private PointPath pointToward;
    private StackPlayer stackManager;
    private ViewsPlayer viewsManager;
    private void Awake()
    {
        stackManager = GetComponentInChildren<StackPlayer>();
        viewsManager = GetComponentInChildren<ViewsPlayer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (!startGame)
            {
                startGame = true;
                GameManager.Instance.ActionStartGame.Invoke();
            }
            touchStart = true;
            pointB = Input.mousePosition;
        }
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (!isEnd && touchStart && !isMoving)
        {
            offset = pointB - pointA;
            pointA = pointB;
            pointToward = PointManager.Instance.GetCurPoint(offset);
            if (pointToward != null)
            {
                isMoving = true;
            }
            
        }
        if (isMoving)
        {
            var step = speed * Time.fixedDeltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, pointToward.transform.position, step);
            if(Vector3.Distance(transform.position, pointToward.transform.position) < 0.0001f)
            {
                if (pointToward.transform.position == pointToward.NextPoint)
                {
                    if (!isEnd)
                    {
                        pointToward = PointManager.Instance.GetEndPoint();
                        isEnd = true;
                    }
                    else
                    {
                        print("ENd Game");
                        GameManager.Instance.ActionEndGame.Invoke();
                        isMoving = false;
                    }
                }else if(pointToward.isContinuos && PointManager.Instance.GetCurPoint(pointToward.NextPoint - pointToward.transform.position) != null)
                {
                    pointToward = PointManager.Instance.GetCurPoint(pointToward.NextPoint - pointToward.transform.position);
                }
                else
                {
                    isMoving = false;
                }
            }
        }
    }

    public void SetHeight(int newHeight)
    {
        viewsManager.SetHeight(newHeight);
        stackManager.SetHeight(newHeight);
    }
}
