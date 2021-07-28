using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        public int dmg = 40;

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

    public Transform deathPartcle;
    public float shakeAmount = 0.1f;
    public float shakeLength = 0.1f;

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

        if (deathPartcle == null)
        {
            Debug.LogError("No death particles on enemy");
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

    void OnCollisionEnter2D(Collision2D _collIfo)
    {
        Player _player = _collIfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.dmg);
            DamageEnemy(99999);
        }

    }
}
