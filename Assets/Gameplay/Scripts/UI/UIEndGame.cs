using UnityEngine.SceneManagement;

public class UIEndGame : UIInstance
{
    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void NextGame()
    {
        DataManager.SetLevel(DataManager.GetLevel() + 1);
        SceneManager.LoadScene(0);
    }
}
