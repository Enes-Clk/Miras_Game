using DG.Tweening;
using UnityEngine;

public class Characteranimationgroundcheck : MonoBehaviour
{
    [Header("Sprite Ayarlarý")]
    public SpriteRenderer spriteRenderer; // Sprite Renderer
    public Sprite[] jumpSprites;         // Zýplama sprite'larý

    [Header("Animasyon Ayarlarý")]
    public float frameRate = 0.1f;      // Kaç saniyede bir frame deðiþecek

    [Header("Zýplama Hareketi")]
    public float jumpHeight = 2f;       // Ne kadar yükseðe zýplasýn
    public float jumpDuration = 0.5f;   // Zýplama süresi (yarýsý yukarý, yarýsý aþaðý)

    [Header("Zemin Kontrolü")]
    public Transform groundCheck;       // Zemini kontrol etmek için kullanýlacak bir nokta (Boþ bir GameObject olabilir)
    public float groundCheckRadius = 0.2f; // Zemin kontrolü için kullanýlacak yarýçap
    public LayerMask whatIsGround;      // Zemin olarak kabul edilecek layer(lar)

    private int currentFrame = 0;
    private bool isJumping = false;
    private Sequence jumpSequence;
    private bool isGrounded;

    void Update()
    {
        // Zemin kontrolünü her frame'de yap
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

        // Zýplama hareketi (yukarý ve geri aþaðý)
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

    // Ýsteðe baðlý: Zemin kontrolünü sahnede görselleþtirmek için (sadece geliþtirme amaçlý)
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

