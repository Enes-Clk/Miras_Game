// Oyuncu bir nesneye çarptığında onu toplar ve yok eder (coin, anahtar vs.)
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Nesne toplandı!");
            Destroy(gameObject);
        }
    }
}