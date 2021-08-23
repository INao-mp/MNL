using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speedOffs = 2f;
    [SerializeField] private LayerMask mask;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float speed)
    {
        rb.velocity = direction.normalized * speedOffs * speed;
        transform.LookAt(direction);
    }
}
