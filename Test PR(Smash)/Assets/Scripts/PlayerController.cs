using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private BallMovement ball;
    [SerializeField] private GameObject image;

    public Transform lookAt;
    private float startDistance;
    private float currDistance;
    private float arrowDistance;
    private bool isGameActive = true;
    private bool canLaunch = true;

    private void Awake()
    {
        startDistance = Vector3.Distance(lookAt.position, transform.position);
        ball = FindObjectOfType<BallMovement>();
    }

    private void Update()
    {
        if (ball == null)
        {
            ball = FindObjectOfType<BallMovement>();
            canLaunch = true;
        }

        if (Time.timeScale < 1f)
        {
            isGameActive = false;
        }else
        {
            StartCoroutine(GameActive());
        }

        if (isGameActive)
        {

            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                arrowDistance = Vector3.Distance(lookAt.position, transform.position);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
                {
                    Vector3 point = raycastHit.point;
                    point.z = Mathf.Clamp(point.z, -4, 4);
                    point.x = Mathf.Clamp(point.x, 5, 9.5f);
                    point.y = 0;
                    transform.position = point;
                    transform.LookAt(lookAt);

                    if (canLaunch)
                    {
                        image.SetActive(true);
                        image.transform.localScale = new Vector3(arrowDistance / 5.5f, 0.15f, 1f);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 dir = (lookAt.position - transform.position).normalized;
                currDistance = Vector3.Distance(lookAt.position, transform.position);
                transform.position = transform.position + (dir * (currDistance - 1f));

                image.SetActive(false);
                if (canLaunch)
                {
                    ball.Launch(dir, currDistance);
                    canLaunch = false;
                }
            }
        }
    }

    IEnumerator GameActive()
    {
        yield return new WaitForSeconds(1.5f);
        isGameActive = true;
    }
}

