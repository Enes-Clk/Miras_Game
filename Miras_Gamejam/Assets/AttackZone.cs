using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public HealthComponent Target;
    public int DamageAmount = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            Target = other.gameObject.GetComponent<HealthComponent>();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")){
            Target = null;
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Target != null){
                Target.TakeDamage(DamageAmount);
            }
        }
    }
}
