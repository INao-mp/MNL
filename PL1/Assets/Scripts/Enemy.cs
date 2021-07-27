using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statInd;

    void Start()
    {
        stats.Init();

        if (statInd != null)
        {
            statInd.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void DamageEnemy (int damage)
    {
        stats.curHealth -= damage;

        if (stats.curHealth <= 0)
        {
            GM_Main.KillEnemy(this);
            Debug.Log("Enemy Die!");
        }

        if (statInd != null)
        {
            statInd.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
}
