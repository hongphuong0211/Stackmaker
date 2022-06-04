using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPlayer : MonoBehaviour
{
    public GameObject stackPrefabs;
    public int maxStack = 100;
    private float deltaHeightStack = 0.3f;
    private List<GameObject> stackList;
    private int curStack;

    private void Awake()
    {
        stackList = new List<GameObject>();
        curStack = 0;
    }

    public void SetHeight(int newCount)
    {
        if (curStack < newCount)
        {
            if (newCount < stackList.Count)
            {
                for (int i = curStack; i < newCount; i++)
                {
                    stackList[i].SetActive(true);
                }
            }
            else
            {
                for (int i = curStack; i < stackList.Count; i++)
                {
                    stackList[i].SetActive(true);
                }
                for (int i = stackList.Count; i < newCount; i++)
                {
                    GameObject stack = Instantiate(stackPrefabs, transform);
                    stack.transform.localPosition = Vector3.up * i * deltaHeightStack;
                    stack.SetActive(true);
                    stackList.Add(stack);
                }
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
