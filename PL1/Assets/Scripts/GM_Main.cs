using UnityEngine;
using System.Collections;


public class GM_Main : MonoBehaviour
{
    public static GM_Main gm;

    private static int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }    
    }

    public int spawnDelay = 2;
    public Transform playerPrefabs;
    public Transform spawnPoint;
    public Transform spawnPrefabs;
    public CameraShake camShake;
    public AudioSource _audio;

    void Start()
    {
        if (camShake == null)
        {
            Debug.LogError("nu camera at GM");
        }    
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }

    public IEnumerator RespawnPlayer()
    {
        _audio.Play();
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefabs, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefabs, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
        gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        Transform _clone =  (Transform)Instantiate(_enemy.deathPartcle, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone.gameObject, 3f);
        camShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
