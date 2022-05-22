using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPath : MonoBehaviour
{
    private bool hasStack;
    private bool loseStack;
    public bool HasStack {
        get { return hasStack; }
        private set { hasStack = value; }
    }
    public bool LoseStack
    {
        get { return loseStack; }
        private set { loseStack = value; }
    }
    public Material colorWhite;
    public Material colorYellow;
    private MeshRenderer colorPath;
    private GameObject stackObject;

    private void Awake()
    {
        stackObject = transform.GetChild(0).gameObject;
        colorPath = transform.GetChild(2).GetComponent<MeshRenderer>();
        hasStack = true;
        loseStack = false;
    }

    public void SetStatusPath(bool newStatusHave, bool newStatusLose = false)
    {
        HasStack = newStatusHave;
        LoseStack = newStatusLose;
        if (loseStack)
        {
            stackObject.SetActive(false);
            if (!hasStack)
            {
                colorPath.material = colorWhite;
            }
            else
            {
                colorPath.material = colorYellow;
            }
        }
        else
        {
            colorPath.material = colorWhite;
            if (!hasStack)
            {
                stackObject.SetActive(false);
            }
            else
            {
                stackObject.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (loseStack && !hasStack)
            {
                GameManager.Instance.CountStack--;
                SetStatusPath(true, true);
            }else if(!loseStack && hasStack)
            {
                GameManager.Instance.CountStack++;
                SetStatusPath(false, false);
            }
        }
    }
}
