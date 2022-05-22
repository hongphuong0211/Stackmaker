using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsPlayer : MonoBehaviour
{
    public void SetHeight(int newHeight)
    {
        transform.localPosition = Vector3.up * newHeight * 0.3f;
    }
}
