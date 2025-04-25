// Oyuncunun canını azaltır, can sıfır olursa oyuncuyu öldürür
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Kalan can: " + health);
        if (health <= 0)
        {
            Debug.Log("Oyuncu öldü.");
            // Ölüm animasyonu veya sahne sıfırlama burada yapılabilir
        }
    }
}