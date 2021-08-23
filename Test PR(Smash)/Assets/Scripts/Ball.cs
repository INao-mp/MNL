using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dOffset = 0.3f;
    [SerializeField] private Transform particles;

    private Rigidbody rb;
    private bool time = false;
    private bool startMoving = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > 2f)
        {
            startMoving = false;
        }

        if (!time)
        {
            time = true;
            StartCoroutine(Velocity());
        }

        if (!startMoving)
        {
            if (rb.velocity.magnitude < dOffset)
            {
                StartCoroutine(Explose());
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
            Transform deathEff = (Transform)Instantiate(particles, transform.position, transform.rotation);
            Destroy(deathEff.gameObject, 1.5f);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            Transform deathEff = (Transform)Instantiate(particles, transform.position, transform.rotation);
            Destroy(deathEff.gameObject, 1.5f);
        }
    }

    IEnumerator Velocity()
    {
        yield return new WaitForSeconds(1f);
        time = false;
    }

    IEnumerator Explose()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        Transform deathEff = (Transform)Instantiate(particles, transform.position, transform.rotation);
        Destroy(deathEff.gameObject, 1.5f);
    }
}
