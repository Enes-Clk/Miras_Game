using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    public int Health = 10;

    public void TakeDamage(int amount){
        Health -= amount;

        if(Health <= 0){

            SceneManager.LoadScene("Heal");
            
        }
    }
}
