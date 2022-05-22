using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { 
        get { 
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance; 
        } 
    }
    private bool endGame = false;
    public bool EndGame
    {
        get { return endGame; }
    }

    public UnityAction ActionEndGame;
    public UnityAction ActionStartGame;
    private int countStack = 0;
    public int CountStack { 
        get { return countStack; }
        set { 
            countStack = value;
            PlayerManager.Instance.SetHeight(value);
        }
    }
    //private void Awake()
    //{
    //    DontDestroyOnLoad(Instance);
    //}
    private void Start()
    {
        ActionStartGame += ()=>{ endGame = false; };
        ActionEndGame += ()=>{ endGame = true; };
    }

}
