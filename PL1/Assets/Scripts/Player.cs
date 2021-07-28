using UnityEngine;

public class Player : MonoBehaviour
{   [System.Serializable]
    public class PlayerStats
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

    public PlayerStats stats = new PlayerStats();

    public int fallPos = -20;

    [SerializeField]
    private StatusIndicator statInd;

    private void Start()
    {
        stats.Init();

        if (statInd == null)
        {
            Debug.LogError("No stat indicator ref on player");
        }
        else
        {
            statInd.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    private void Update()
    {
        if (transform.position.y<= fallPos)
        {
            DamagePlayer(99999);
        }
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;

        if (stats.curHealth <= 0)
        {
            GM_Main.KillPlayer(this);
            Debug.Log("You Die!");
        }

        statInd.SetHealth(stats.curHealth, stats.maxHealth);
    }

}
