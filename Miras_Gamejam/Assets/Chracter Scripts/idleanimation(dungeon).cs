using DG.Tweening;
using UnityEngine;

public class CharacterIdleAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] idleSprites;
    public float frameRate = 0.3f; // Idle animasyon daha yavaş akar

    private int currentFrame = 0;
    private Sequence idleSequence;
    private bool isIdling = false;

    void Start()
    {
        PlayIdleAnimation(); // Başlangıçta idle animasyon başlasın
    }

    public void PlayIdleAnimation()
    {
        if (idleSequence != null) idleSequence.Kill();
        currentFrame = 0;
        isIdling = true;

        idleSequence = DOTween.Sequence();

        for (int i = 0; i < idleSprites.Length; i++)
        {
            idleSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = idleSprites[currentFrame];
                currentFrame = (currentFrame + 1) % idleSprites.Length;
            });
            idleSequence.AppendInterval(frameRate);
        }

        idleSequence.SetLoops(-1); // Sonsuz döngü
    }

    public void StopIdleAnimation()
    {
        if (idleSequence != null) idleSequence.Kill();
        isIdling = false;

        if (idleSprites.Length > 0)
        {
            spriteRenderer.sprite = idleSprites[0]; // İlk idle sprite'da dur
        }
    }
}
