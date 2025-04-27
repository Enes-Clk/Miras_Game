using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    public int damage = 5; // Verilecek hasar
    public float lifeTime = 2f; // Yok olma süresi

    void Start()
    {
        Destroy(gameObject, lifeTime); // 5 saniye sonra kendi kendine yok olsun
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         // Player'ın bir Health componenti varsa, hasar ver
    //         playerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //         if (playerHealth != null)
    //         {
    //             playerHealth.TakeDamage(damage);
    //         }

    //         Destroy(gameObject); // Çarptıktan sonra top yok olsun
    //     }
    // }
}
