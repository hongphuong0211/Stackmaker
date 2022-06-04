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
        GameManager.Instance.ChangeState(EnumManager.GameState.Pause);
        Time.timeScale = 0;
        UIPause.gameObject.SetActive(true);
    }

    public void ContinueGame(){
        GameManager.Instance.ChangeState(EnumManager.GameState.GamePlay);
        UIPause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReplayGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
