
using UnityEngine;

public class Winning : MonoBehaviour
{
    public GameManager gameManager;
    void  OnTriggerEnter ()
        {
        gameManager.WinGame();
        }
}
