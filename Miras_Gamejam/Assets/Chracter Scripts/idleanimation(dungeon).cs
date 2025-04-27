
using System.Collections;
using UnityEngine;

public class CharacterIdleAnimation : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    [Header("Saldırı Ayarları")]
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Referanslar")]
    public Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // Durum Kontrolü
    private bool isPlayerDetected = false;
    private bool isAttacking = false;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Eğer player referansı atanmamışsa, sahnede "Player" tag'ine sahip objeyi bul
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Oyuncu ile düşman arasındaki mesafeyi hesapla
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Oyuncu algılama menzilinde mi kontrol et
            if (distanceToPlayer <= detectionRange)
            {
                isPlayerDetected = true;
                
                // Oyuncuya doğru bak (flipset kullanımı)
                if (player.position.x > transform.position.x)
                {
                    spriteRenderer.flipX = false; // Sağa bak
                }
                else
                {
                    spriteRenderer.flipX = true; // Sola bak
                }
                
                // Saldırı menzilinde mi kontrol et
                if (distanceToPlayer <= attackRange)
                {
                    // Saldırı animasyonunu başlat ve hareket etmeyi durdur
                    if (canAttack && !isAttacking)
                    {
                        // StartCoroutine(AttackPlayer());
                    }
                }
                else
                {
                    // Oyuncuya doğru hareket et
                    MoveTowardsPlayer();
                }
            }
            else
            {
                // Oyuncu menzil dışında, idle durumuna geç
                isPlayerDetected = false;
                animator.SetBool("IsMoving", false);
                PlayIdleAnimation();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isAttacking)
        {
            // Oyuncuya doğru hareket vektörü
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Hareket
            rb.velocity = direction * moveSpeed;
            
            // Yürüme animasyonu
            animator.SetBool("IsMoving", true);
        }
    }

    // private IEnumerator AttackPlayer()
    // {
    //     // Saldırı durumunu güncelle
    //     isAttacking = true;
    //     canAttack = false;
    //     rb.velocity = Vector2.zero; // Hareketi durdur
        
    //     // Saldırı animasyonu
    //     animator.SetTrigger("Attack");
        
    //     // Saldırı animasyonu süresince bekle (animasyona göre ayarlanmalı)
    //     yield return new WaitForSeconds(0.5f); // Saldırı animasyonunun vuruş anı
        
    //     // Menzil kontrolü tekrar yap (oyuncu kaçmış olabilir)
    //     if (Vector2.Distance(transform.position, player.position) <= attackRange)
    //     {
    //         // Oyuncuya hasar ver
    //         PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
    //         if (playerHealth != null)
    //         {
    //             playerHealth.TakeDamage(attackDamage);
    //         }
    //     }
        
    //     // Saldırı animasyonunun geri kalanını bekle
    //     yield return new WaitForSeconds(0.5f);
        
    //     // Saldırı durumunu güncelle
    //     isAttacking = false;
        
    //     // Cooldown süresi
    //     yield return new WaitForSeconds(attackCooldown);
    //     canAttack = true;
    // }

    private void PlayIdleAnimation()
    {
        // İki farklı idle animasyonu arasında rastgele seçim yapabilirsiniz
        if (Random.value > 0.95f && !animator.GetBool("IdleAlt"))
        {
            animator.SetBool("IdleAlt", true);
        }
        else if (Random.value > 0.95f && animator.GetBool("IdleAlt"))
        {
            animator.SetBool("IdleAlt", false);
        }
    }

    // Animatörünüze aşağıdaki parametreleri eklemeniz gerekecek:
    // - IsMoving (bool): Hareket etme durumu
    // - Attack (trigger): Saldırı tetikleyicisi
    // - IdleAlt (bool): Alternatif idle animasyonu için
}
