using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;

    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave +1; }
    }

    public Transform[] spawnPoint;
    public float TBW = 5f;  // Time between Waves

    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCount = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    private void Start()
    {
        waveCountdown = TBW;    
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Done!");
        state = SpawnState.COUNTING;
        waveCountdown = TBW;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Complete all LvL! Loooping...");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCount -= Time.deltaTime;

        if (searchCount <= 0f)
        {
            searchCount = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }

        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("SP wave " + _wave.name);

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemies(_wave.enemy);
            yield return new WaitForSeconds (1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;

    }

    void SpawnEnemies(Transform _enemy)
    {
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        //Debug.Log("Spawning enemy ^" + _enemy.name);
    }

}
