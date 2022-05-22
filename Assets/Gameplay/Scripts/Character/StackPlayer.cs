using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPlayer : MonoBehaviour
{
    public GameObject stackPrefabs;
    public int maxStack = 100;
    private List<GameObject> stackList;
    private int curStack;

    private void Awake()
    {
        stackList = new List<GameObject>();
        for(int i = 0; i <maxStack; i++)
        {
            GameObject stack = Instantiate(stackPrefabs, transform);
            stack.transform.localPosition = Vector3.up * i * 0.3f;
            stack.SetActive(false);
            stackList.Add(stack);
        }
        curStack = 0;
    }

    public void SetHeight(int newCount)
    {
        if (curStack < newCount)
        {
            for (int i = curStack; i < newCount; i++)
            {
                stackList[i].SetActive(true);
            }
        }
        else
        {
            for (int i = newCount; i < curStack; i++)
            {
                stackList[i].SetActive(false);
            }
        }
        curStack = newCount;
    }
}
