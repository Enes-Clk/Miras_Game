using UnityEngine;

public class SwordAttackk : MonoBehaviour
{
    public int damageAmount = 10; // Kılıcın vereceği hasar
    private bool isCollidingWithEnemy = false; // Kılıç düşmana çarpıyor mu?

    void Update()
    {
        // Sol mouse tuşuna basıldığında
        if (Input.GetMouseButtonDown(0)) // 0, sol mouse tuşudur.
        {
            if (isCollidingWithEnemy)
            {
                // Kılıç düşmanla çarpışıyorsa, düşmana hasar ver
                DealDamageToEnemy();
            }
        }
    }

    // Collider ile temas edilen düşmanı bulma ve ona hasar verme
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Düşmanla çarpışma kontrolü
        {
            isCollidingWithEnemy = true; // Düşmana temas edildi
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Düşmanla temas bittiğinde
        {
            isCollidingWithEnemy = false; // Düşmana temas sonlandı
        }
    }

    void DealDamageToEnemy()
    {
        // Kılıçla çarpışan düşmanla ilgili işlem
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0f);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Düşmana hasar uygula
                Damageable damageable = enemy.GetComponent<Damageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                }
            }
        }
    }
}
