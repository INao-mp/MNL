using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad = "Level01";
    public string SelectLEvel = "levelSelector";

    public SceneFade sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(LevelToLoad);
    }

    public void Select()
    {
        sceneFader.FadeTo(SelectLEvel);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
