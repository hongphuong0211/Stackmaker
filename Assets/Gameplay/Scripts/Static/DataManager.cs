using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : PlayerPrefs
{
    public static void SetLevel(int newLevel)
    {
        if (newLevel < 0)
        {
            return;
        }
        SetInt(ConstantManager.DATA_LEVEL, newLevel);
    }
    public static int GetLevel()
    {
        return GetInt(ConstantManager.DATA_LEVEL, 0);
    }
}
