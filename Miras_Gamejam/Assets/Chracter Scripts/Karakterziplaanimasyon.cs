using DG.Tweening;
using UnityEngine;

public class CharacterJumpAnimation : MonoBehaviour
{
    [Header("Sprite Ayarları")]
    public SpriteRenderer spriteRenderer; // Sprite Renderer
    public Sprite[] jumpSprites;           // Zıplama sprite'ları

    [Header("Animasyon Ayarları")]
    public float frameRate = 0.1f;          // Kaç saniyede bir frame değişecek

    [Header("Zıplama Hareketi")]
    public float jumpHeight = 2f;           // Ne kadar yükseğe zıplasın
    public float jumpDuration = 0.5f;       // Zıplama süresi (yarısı yukarı, yarısı aşağı)

    private int currentFrame = 0;
    private bool isJumping = false;
    private Sequence jumpSequence;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
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

        // Zıplama hareketi (yukarı ve geri aşağı)
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
}

