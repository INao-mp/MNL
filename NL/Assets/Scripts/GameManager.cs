using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool GameHasEnd = false;
    public float TTR = 1f;
    public GameObject ComleteLvlUI;
    public void WinGame()
    {
        ComleteLvlUI.SetActive(true);
    }
    public void EndGame()
    {
        if (GameHasEnd == false)
        {
            GameHasEnd = true;
            Debug.Log("Game Over");
            Invoke("reset", TTR);
        }

    }
    void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
