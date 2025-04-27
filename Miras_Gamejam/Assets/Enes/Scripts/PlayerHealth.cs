// Oyuncunun canını azaltır, can sıfır olursa oyuncuyu öldürür
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] int PlayerHealth;

    void Start()
    {
        PlayerHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        PlayerHealth -= amount;
        Debug.Log("Player Health: " + PlayerHealth);

        if (PlayerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player öldü!");
        SceneManager.LoadScene("Heal");
        // Ölüm animasyonu veya oyun sonu buraya eklenebilir
    }
    }
