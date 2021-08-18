using UnityEngine;

public class Controller : MonoBehaviour
{
    
    [SerializeField] private int offs = 1200;
    [SerializeField] private int bOffs = 500;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    private Vector3 startP;
    private Quaternion startR;

    private void Awake()
    {
        startP = transform.position;
        startR = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.forward * offs * Time.deltaTime);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                transform.position = raycastHit.point;
            }

        }
        else
        {
            Vector3 dir = (startP - transform.position).normalized;
            if (Vector3.Distance(startP, transform.position) <= 0.3f)
            {
                transform.position = startP;
                transform.rotation = startR;
            }
            else
            {
                transform.position += dir * bOffs * Time.deltaTime;
                transform.rotation = startR;
            }
        }
    }
}
