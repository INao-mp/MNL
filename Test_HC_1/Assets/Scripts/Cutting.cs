using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] private DanceStart stick;
    [SerializeField] private Transform particleSpawnPoint;
    [SerializeField] private Transform particles;
    [SerializeField] private float deathTimer = 0;
    [SerializeField] private bool scale = false;

    private int hitTime = 0;
    private float cutTime = 0;

    private void Update()
    {
        cutTime -= 3/Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Knife") && cutTime <= 0)
        {
            if (scale == true)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            }

            Transform cutEff = (Transform)Instantiate(particles, particleSpawnPoint.position, particleSpawnPoint.rotation);
            Destroy(cutEff.gameObject, 2f);
            hitTime++;
            cutTime = 1f;

            if (hitTime == deathTimer)
            {
                Destroy(this.gameObject);
                if (this.gameObject.CompareTag("BasePlate"))
                {
                    stick.timer--;
                }
            }
        }
    }
}
