using UnityEngine;

public class animornek : MonoBehaviour
{
  
    private Animator animator;
    
    
  
    // Mermi oluşturma zamanını kontrol etmek için değişkenler
 

    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    
    
    
    

    void Update()
    {
       

            // Animator'deki parametre tetikleniyor
            animator.SetTrigger("isAttacking");
        }
    }





