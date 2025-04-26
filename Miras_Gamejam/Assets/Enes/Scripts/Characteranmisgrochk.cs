using DG.Tweening;
using UnityEngine;

public class Characteranimationgroundcheck : MonoBehaviour
{
    [Header("Sprite Ayarlar�")]
    public SpriteRenderer spriteRenderer; // Sprite Renderer
    public Sprite[] jumpSprites;         // Z�plama sprite'lar�

    [Header("Animasyon Ayarlar�")]
    public float frameRate = 0.1f;      // Ka� saniyede bir frame de�i�ecek

    [Header("Z�plama Hareketi")]
    public float jumpHeight = 2f;       // Ne kadar y�kse�e z�plas�n
    public float jumpDuration = 0.5f;   // Z�plama s�resi (yar�s� yukar�, yar�s� a�a��)

    [Header("Zemin Kontrol�")]
    public Transform groundCheck;       // Zemini kontrol etmek i�in kullan�lacak bir nokta (Bo� bir GameObject olabilir)
    public float groundCheckRadius = 0.2f; // Zemin kontrol� i�in kullan�lacak yar��ap
    public LayerMask whatIsGround;      // Zemin olarak kabul edilecek layer(lar)

    private int currentFrame = 0;
    private bool isJumping = false;
    private Sequence jumpSequence;
    private bool isGrounded;

    void Update()
    {
        // Zemin kontrol�n� her frame'de yap
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            PlayJumpAnimation();
        }
    }

    void PlayJumpAnimation()
    {
        isJumping = true;
        currentFrame = 0;

        // Sprite animasyonu
        jumpSequence = DOTween.Sequence();

        for (int i = 0; i < jumpSprites.Length; i++)
        {
            jumpSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = jumpSprites[currentFrame];
                currentFrame = (currentFrame + 1) % jumpSprites.Length;
            });
            jumpSequence.AppendInterval(frameRate);
        }

        // Z�plama hareketi (yukar� ve geri a�a��)
        float originalY = transform.position.y;

        transform.DOMoveY(originalY + jumpHeight, jumpDuration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOMoveY(originalY, jumpDuration / 2)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        isJumping = false;
                    });
            });
    }

    // �ste�e ba�l�: Zemin kontrol�n� sahnede g�rselle�tirmek i�in (sadece geli�tirme ama�l�)
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

