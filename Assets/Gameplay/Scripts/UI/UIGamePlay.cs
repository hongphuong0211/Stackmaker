using UnityEngine;
using UnityEngine.SceneManagement;
public class UIGamePlay : UIInstance
{
    public GameObject UIPause;
    protected override void OnEnable()
    {
        base.OnEnable();
        UIPause.gameObject.SetActive(false);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        UIPause.gameObject.SetActive(true);
    }

    public void ContinueGame(){
        UIPause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReplayGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
