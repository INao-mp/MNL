using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLvL : MonoBehaviour
{
    public SceneFade sceneFader;

    public string menuSceneName = "Menu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
