using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private SceneFade sceneFader;
    private string menuSceneName = "Menu";

    private void Awake()
    {
       StartCoroutine(Begin());
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.SetActive(true);
        }
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(.5f);
        Toggle();
        Toggle();
    }
}
