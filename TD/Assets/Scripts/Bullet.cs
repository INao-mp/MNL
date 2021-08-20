using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public int damage = 50;
    public GameObject bulletImpact;

    public void seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceToFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceToFrame)
        {
            HitTheTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceToFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTheTarget()
    {
        GameObject effInst = (GameObject)Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(effInst, 5f);

        if (explosionRadius >= 0)
        {
            Explode();
        }else
        {
            Damage(target);
        }
       
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }  
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
