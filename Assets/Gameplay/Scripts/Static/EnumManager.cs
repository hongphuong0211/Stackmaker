using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{
    public enum DataType
    {
        level,

    }

    public enum NumberUI
    {
        MainMenu,
        Shop,
        Settings,
        GamePlay,
        Pause,
        Results
    }

    public enum StatePlayer
    {
        Idle,
        Jump,
        Win,
    }

    public enum GameState{
        GameMenu,
        Pause,
        GamePlay,
        EndGame,
    }
}
