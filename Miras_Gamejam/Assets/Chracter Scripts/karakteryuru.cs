using DG.Tweening;
using UnityEngine;

public class CharacterWalkAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] walkSprites;  // Yürüyüş animasyonu
    public Sprite[] idleSprites;  // Idle animasyonu
    public Sprite[] jumpSprites;  // Zıplama animasyonu (opsiyonel)
    public float frameRate = 0.1f;
    public float moveSpeed = 5f;  // Yürüyüş hızı
    public float jumpForce = 5f;  // Zıplama gücü

    private int currentFrame = 0;
    private Sequence walkSequence;
    private bool isWalking = false;
    private bool isJumping = false;

    private Rigidbody2D rb;  // Rigidbody2D kullanarak hareketi kontrol et
    private bool isGrounded = true; // Yerle temas kontrolü

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D'yi başlatıyoruz
    }

    void Update()
    {
        CheckGroundStatus();  // Yere temas durumunu kontrol et

        bool isPressingAD = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool isPressingSpace = Input.GetKeyDown(KeyCode.Space);  // Space tuşuna basma kontrolü

        // Karakter havadaysa animasyon çalışmasın
        if (!isGrounded)
        {
            if (isWalking)
            {
                StopWalkAnimation();
            }
            return;  // Havada ise animasyon devam etmesin ve hareket etmeyecek
        }

        // Yürüyüş animasyonu için A ve D tuşları
        if (isPressingAD && !isJumping)
        {
            if (!isWalking)
            {
                PlayWalkAnimation();
            }

            // Yönü ayarla
            if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = true;  // sola bak
                MoveCharacter(-1);  // Sola hareket et
            }
            else if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = false;  // sağa bak
                MoveCharacter(1);  // Sağa hareket et
            }
        }
        else
        {
            if (!isJumping)
            {
                PlayIdleAnimation();  // Yürüyüş yoksa idle animasyonunu başlat
            }
        }

        // Zıplama
        if (isPressingSpace && isGrounded)
        {
            Jump();  // Zıplama fonksiyonunu çağır
        }
    }

    // Yere temas edip etmediğini kontrol et
    void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);  // Yere temas kontrolü
    }

    // Karakteri hareket ettir
    void MoveCharacter(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);  // X ekseninde hareket et
    }

    // Yürüyüş animasyonunu başlat
    void PlayWalkAnimation()
    {
        if (walkSequence != null) walkSequence.Kill();
        currentFrame = 0;
        isWalking = true;

        walkSequence = DOTween.Sequence();

        for (int i = 0; i < walkSprites.Length; i++)
        {
            walkSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = walkSprites[currentFrame];
                currentFrame = (currentFrame + 1) % walkSprites.Length;
            });
            walkSequence.AppendInterval(frameRate);
        }

        walkSequence.SetLoops(-1);  // Sürekli döngü
    }

    // Yürüyüş animasyonunu durdur
    void StopWalkAnimation()
    {
        if (walkSequence != null) walkSequence.Kill();
        isWalking = false;

        if (walkSprites.Length > 0)
        {
            spriteRenderer.sprite = walkSprites[0];  // İlk frame'e sabitler (Idle gibi)
        }
    }

    // Idle animasyonunu başlat
    void PlayIdleAnimation()
    {
        if (isWalking)
        {
            StopWalkAnimation();
        }

        if (idleSprites.Length > 0)
        {
            spriteRenderer.sprite = idleSprites[0];  // Idle animasyonu ile sabit bir frame
        }
    }

    // Zıplama fonksiyonu
    void Jump()
    {
        if (isJumping) return;  // Eğer zaten zıplıyorsa, tekrar zıplamayı engelle

        isJumping = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Zıplama gücü ekleniyor
        StopWalkAnimation();  // Yürüyüş animasyonunu durdur
        // Zıplama animasyonunu burada başlatabilirsiniz (Opsiyonel)
    }

    // Zıplama bittiğinde karakter yere inerse animasyonu başlat
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping)
        {
            isJumping = false;  // Zıplama bitiyor
            PlayIdleAnimation();  // Yürüyüş animasyonu yerine Idle animasyonuna geçiş yap
        }
    }
}
