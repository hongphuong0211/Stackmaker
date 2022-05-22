public class UIMainMenu : UIInstance
{
    protected override void OnInit()
    {
        
    }
    public void OpenShop()
    {
        UIManager.Instance.OpenUI(EnumManager.NumberUI.Shop);
    }

    public void OpenSettings()
    {
        UIManager.Instance.OpenUI(EnumManager.NumberUI.Settings);
    }
}
