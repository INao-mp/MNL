using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPosition;
    public Text waveCountdownText;
    public GameMaster gameMaster;

    public float TTspawn = 5f;
    private float TFspawn = 2f;

    private int waveIndex = 0;

    void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameMaster.WinLevel();
            this.enabled = false;
        }

        if (TFspawn <= 0)
        {
            StartCoroutine(SpawnWave());
            TFspawn = TTspawn;
            return;
        }

        TFspawn -= Time.deltaTime;

        TFspawn = Mathf.Clamp(TFspawn, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", TFspawn);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            spawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);

        }

        waveIndex++;

    }
    void spawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
    }

}
