using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsPlayer : MonoBehaviour
{
    public Animator animPlayer;
    private float deltaHeightStack = 0.3f;

    private void Awake(){
        GameManager.Instance.ActionEndGame += ()=>{SetStateAnim(EnumManager.StatePlayer.Win);};
    }

    public void SetHeight(int newHeight)
    {
        SetStateAnim(EnumManager.StatePlayer.Jump);
        transform.localPosition = Vector3.up * newHeight * deltaHeightStack;
    }

    public void SetStateAnim(int number){
        animPlayer.SetInteger(ConstantManager.ACTION_PLAYER, number);
    }
    public void SetStateAnim(EnumManager.StatePlayer state){
        animPlayer.SetInteger(ConstantManager.ACTION_PLAYER, (int)state);
    }
}

