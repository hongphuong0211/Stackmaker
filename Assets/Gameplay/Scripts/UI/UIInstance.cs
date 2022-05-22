using UnityEngine;

public class UIInstance : MonoBehaviour
{
    public EnumManager.NumberUI numberUI;
    public EnumManager.NumberUI otherNumberUI;
    protected virtual void OnInit()
    {

    }
    protected virtual void OnEnable()
    {
        if (otherNumberUI != numberUI)
        {
            UIManager.Instance.OpenUI(otherNumberUI);
        }
    }
    protected virtual void OnDisable()
    {
        if (otherNumberUI != numberUI)
        {
            UIManager.Instance.CloseUI(otherNumberUI);
        }
    }
    

}
