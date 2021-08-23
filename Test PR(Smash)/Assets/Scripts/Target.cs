using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private TargetsSpawn TS;
    [SerializeField] private Transform particles;

    private void Start()
    {
        TS = FindObjectOfType<TargetsSpawn>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TS.targetsCount--;
            Destroy(this.gameObject);
            Transform deathEff = (Transform)Instantiate(particles, transform.position, transform.rotation);
            Destroy(deathEff.gameObject, 1.5f);
        }
    }
}
