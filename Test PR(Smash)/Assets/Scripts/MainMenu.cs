using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFader;
    private string menuSceneName = "MainLevel";

    public void PlayGame()
    {
        sceneFader.FadeTo(menuSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
