using UnityEngine;

public class DanceStart : MonoBehaviour
{
    private Animator animator;
    public int timer = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (timer <= 0 )
        {
            animator.enabled = true;
        }   
    }
}
