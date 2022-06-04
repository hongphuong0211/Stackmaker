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
    private EnumManager.GameState gameState;

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
        gameState = EnumManager.GameState.GameMenu;
        ActionStartGame += ()=>{ ChangeState(EnumManager.GameState.GamePlay);};
        ActionEndGame += ()=>{ ChangeState(EnumManager.GameState.EndGame); };
    }

    public void ChangeState(EnumManager.GameState newState){
        if(gameState != newState){
            gameState = newState;
        }
    }

    public bool IsState(EnumManager.GameState checkState){
        return gameState == checkState;
    }
}


