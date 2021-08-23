using UnityEngine;
using System.Collections;

public class TargetsSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] targetSpawnPoints;
    [SerializeField] private Transform ballSpawnPoints;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private EnemyAI enemyAI;

    [HideInInspector]
    public int targetsCount = 3;

    private Transform ball;
    private bool searchForBall = false;

    private void Update()
    {
        if (targetsCount == 0)
        {
            StartCoroutine(SpawnTarget());
            LevelStatus.instance.UpLevel();
            enemyAI.ChangeDifficult();
            targetsCount = 3;
        }

        if (ball == null)
        {
            if (!searchForBall)
            {
                searchForBall = true;
                StartCoroutine(Spawnball());
            }
            return;
        }
    }

    IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < targetSpawnPoints.Length; i++)
        {
            Instantiate(targetPrefab, targetSpawnPoints[i].position, targetSpawnPoints[i].rotation);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator Spawnball()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Ball");

        if (sResult == null)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(ballPrefab, ballSpawnPoints.position, ballSpawnPoints.rotation);
            StartCoroutine(Spawnball());
        }
        else
        {
            ball = sResult.transform;
            searchForBall = false;
            yield return false;
        }
    }


}
