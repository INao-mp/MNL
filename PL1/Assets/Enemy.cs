using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int health = 100;
    }

    public EnemyStats stats = new EnemyStats();

    public void DamageEnemy(int damage)
    {
        stats.health -= damage;

        if (stats.health <= 0)
        {
            GM_Main.KillEnemy(this);
            Debug.Log("Enemy Die!");
        }
    }
}
