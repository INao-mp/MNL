using UnityEngine;

public class MoveTrial : MonoBehaviour
{
    public int MoveSpeed = 20;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        Destroy(gameObject, 1);

    }
}
