using UnityEngine;

public class Player : MonoBehaviour
{   [System.Serializable]
    public class PlayerStats
    {
        public int health = 100;
    }

    public PlayerStats playerStats = new PlayerStats();

    public int fallPos = -20;

    private void Update()
    {
        if (transform.position.y<= fallPos)
        {
            DamagePlayer(99999);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStats.health -= damage;

        if (playerStats.health <= 0)
        {
            GM_Main.KillPlayer(this);
            Debug.Log("You Die!");
        }
    }

}
