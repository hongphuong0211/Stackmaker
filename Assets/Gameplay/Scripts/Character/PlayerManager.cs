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

    public float maxSpeed = 5.0f;
    private float deltaSpeed = 0.1f;
    private float speed = 5.0f;
    private float deltaDistance = 0.0001f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private bool isMoving = false;
    private Vector2 offset;
    private Vector2 direction;
    private PointPath pointToward;
    public StackPlayer stackManager;
    public ViewsPlayer viewsManager;
    // private void Awake()
    // {
    //     stackManager = GetComponentInChildren<StackPlayer>();
    //     viewsManager = GetComponentInChildren<ViewsPlayer>();
    // }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (!GameManager.Instance.IsState(EnumManager.GameState.GamePlay))
            {
                GameManager.Instance.ChangeState(EnumManager.GameState.GamePlay);
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
        if (!GameManager.Instance.IsState(EnumManager.GameState.EndGame) && touchStart && !isMoving)
        {
            offset = pointB - pointA;
            pointA = pointB;
            pointToward = PointManager.Instance.GetCurPoint(offset);
            if (pointToward != null)
            {
                isMoving = true;
                speed = maxSpeed;
                deltaSpeed = (transform.position - pointToward.transform.position).magnitude / maxSpeed;
            }
            
        }
        if (isMoving)
        {
            if (speed > 0.01f){
                speed -= deltaSpeed * Time.fixedDeltaTime;
            }
            var step = speed * Time.fixedDeltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, pointToward.transform.position, step);
            if((transform.position - pointToward.transform.position).magnitude < deltaDistance)
            {
                if (pointToward.transform.position == pointToward.NextPoint)
                {
                    if (!GameManager.Instance.IsState(EnumManager.GameState.EndGame))
                    {
                        pointToward = PointManager.Instance.GetEndPoint();
                        GameManager.Instance.ChangeState(EnumManager.GameState.EndGame);
                    }
                    else
                    {
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
        }else if(!GameManager.Instance.IsState(EnumManager.GameState.EndGame)){
            viewsManager.SetStateAnim(EnumManager.StatePlayer.Idle);
        }
    }

    public void SetHeight(int newHeight)
    {
        viewsManager.SetHeight(newHeight);
        stackManager.SetHeight(newHeight);
    }
}
