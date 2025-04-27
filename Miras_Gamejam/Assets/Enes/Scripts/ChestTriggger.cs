using UnityEngine;

public class ChestTriggger : MonoBehaviour
{
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();

    }


    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Play");

        }

    }
}
