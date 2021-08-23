using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private GameObject targetBall;
    private Rigidbody rb;
    private float upSpeed;
    private float distance;
    private bool searchBall;
    public float speed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        upSpeed = speed;

        if (targetBall == null)
        {
            if (!searchBall)
            {
                searchBall = true;
                StartCoroutine(SearchBall());
            }
            return;
        }
    }

    private void FixedUpdate()
    {
        if (targetBall == null)
        {
            if (!searchBall)
            {
                searchBall = true;
                StartCoroutine(SearchBall());
                rb.velocity = new Vector3(0, 0, 0);
            }
            return;
        }

        distance = Mathf.Abs(targetBall.transform.position.z - transform.position.z);
        StartFollow();
    }

    IEnumerator SearchBall()
    {
        targetBall = GameObject.FindGameObjectWithTag("Ball");
        if (targetBall == null)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(SearchBall());         
        }else
        {
            searchBall = false;
            yield return false;
        }
    }

    void StartFollow()
    {
        if (targetBall.transform.position.z < transform.position.z && distance > .3f)
        {
            rb.velocity = new Vector3(0, 0, -speed * Time.deltaTime);
        }
        if (targetBall.transform.position.z > transform.position.z && distance > .3f)
        {
            rb.velocity = new Vector3(0, 0, speed * Time.deltaTime);
        }
    }

    public void ChangeDifficult()
    {
        int test = LevelStatus.instance.Asd();
        speed = upSpeed * test;
    }
}
