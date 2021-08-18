using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private int offs;

    private void Update()
    {
        transform.Rotate(Vector3.up * offs * Time.deltaTime);
    }
}
