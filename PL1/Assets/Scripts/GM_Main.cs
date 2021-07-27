using UnityEngine;
using System.Collections;


public class GM_Main : MonoBehaviour
{
    public static GM_Main gm;

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }    
    }

    public Transform playerPrefabs;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefabs;
    public AudioSource _audio;
    
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
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
