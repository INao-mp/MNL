using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotOffset = 0;

    void Update()
    {
        Vector3 dif = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dif.Normalize();

        float rotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotOffset);
    }
}
