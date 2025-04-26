using DG.Tweening;
using UnityEngine;

public class SwordSwingAnimation : MonoBehaviour
{
    [Header("Sprite Ayarları")]
    public SpriteRenderer spriteRenderer;  // Karakterin Sprite Renderer'ı
    public Sprite[] swingSprites;           // Kılıç savurma sprite'ları

    [Header("Animasyon Ayarları")]
    public float frameRate = 0.05f;          // Frame değiştirme süresi (hızlı olmalı)

    private int currentFrame = 0;
    private bool isSwinging = false;
    private Sequence swingSequence;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging) // Sol klik ile başla
        {
            PlaySwingAnimation();
        }
    }

    void PlaySwingAnimation()
    {
        isSwinging = true;
        currentFrame = 0;

        swingSequence = DOTween.Sequence();

        for (int i = 0; i < swingSprites.Length; i++)
        {
            swingSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = swingSprites[currentFrame];
                currentFrame = (currentFrame + 1) % swingSprites.Length;
            });
            swingSequence.AppendInterval(frameRate);
        }

        swingSequence.OnComplete(() =>
        {
            isSwinging = false; // Animasyon bitince tekrar savurulabilir olsun
        });
    }
}
