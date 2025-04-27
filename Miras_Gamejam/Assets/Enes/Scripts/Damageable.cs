using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 100; // Düşmanın canı
    public GameObject objectToActivate; // Öldüğünde aktif edilecek obje

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " öldü!");
        
        // Eğer bir GameObject atanmışsa onu aktif et
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        
        Destroy(gameObject); // Düşmanı sahneden kaldır
    }
}
