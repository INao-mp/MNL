using UnityEngine;

public class Cutting : MonoBehaviour
{
    private int hitTime = 0;
    private float cutTime = 0;
    [SerializeField] private Transform particles;


    private void Update()
    {
        cutTime -=3/Time.deltaTime;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Knife") && cutTime <=0)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            hitTime++;
            // Debug.Log("EEEEEEE!" + hitTime);
            Transform cutEff = (Transform)Instantiate(particles, transform.position, transform.rotation);
            Destroy(cutEff.gameObject, 2f);
            cutTime = 1f;

            if (hitTime == 3)
            {

                Destroy(this.gameObject);
            }
        }

    }

}
