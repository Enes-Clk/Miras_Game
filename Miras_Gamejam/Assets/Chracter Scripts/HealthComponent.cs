using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int Health = 10;

    public void TakeDamage(int amount){
        Health -= amount;

        if(Health <= 0){
            
        }
    }
}
