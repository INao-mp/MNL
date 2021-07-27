using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemy_AI : MonoBehaviour
{
    public Transform target;            // what chase
    public Path path;                   // calculate path
    public ForceMode2D fMode;
    public float updateReate = 2f;
    public float speed = 300f;
    public float nextWayDis = 3;

    [HideInInspector]
    public bool pathIsEnd = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWayPoint = 0;
    private bool searchForPlayer = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        seeker.StartPath (transform.position, target.position, OnPathComplete);
        StartCoroutine (UpdatePath());
    }

    IEnumerator SearchForPlayer()
    {
        GameObject sResult =  GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = sResult.transform;
            searchForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;

        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }

        seeker.StartPath (transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateReate);
        StartCoroutine (UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Error -> check = " + p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchForPlayer)
            {
                searchForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathIsEnd)
            {
                return;
            }

            Debug.Log("End!");
            pathIsEnd = true;
            return;
        }

        pathIsEnd = false;

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);

        if (dist < nextWayDis)
        {
            currentWayPoint++;
            return;
        }
    }
}
