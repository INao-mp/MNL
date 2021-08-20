using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int MoneyGain = 50;

    public GameObject DEffect;

    [Header("Unity Staff")]
    public Image HealthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        HealthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(DEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

        PlayerStats.Money += MoneyGain;
        Destroy(gameObject);

        GameManager.EnemiesAlive--;
    }
}
