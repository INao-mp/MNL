
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    public Movement move;
   void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.collider.tag == "Wall")
        {
            move.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
