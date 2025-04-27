using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    bool isCanAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (isCanAttack){
                StartCoroutine(WaitForAttack());
            }
            
        }
    }
    
    IEnumerator WaitForAttack(){
        isCanAttack =false;
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(8f);
        isCanAttack = true;
    }

    
}
