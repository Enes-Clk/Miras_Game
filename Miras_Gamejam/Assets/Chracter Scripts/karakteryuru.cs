using DG.Tweening;
using UnityEngine;

public class CharacterWalkAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] walkSprites;
    public float frameRate = 0.1f;

    private int currentFrame = 0;
    private Sequence walkSequence;
    private bool isWalking = false;

    void Update()
    {
        bool isPressingAD = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        if (isPressingAD)
        {
            if (!isWalking)
            {
                PlayWalkAnimation();
            }

            // Yönü ayarla
            if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = true; // sola bak
            }
            else if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = false; // sağa bak
            }
        }
        else
        {
            if (isWalking)
            {
                StopWalkAnimation();
            }
        }
    }

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

        walkSequence.SetLoops(-1); // Sürekli döngü
    }

    void StopWalkAnimation()
    {
        if (walkSequence != null) walkSequence.Kill();
        isWalking = false;

        if (walkSprites.Length > 0)
        {
            spriteRenderer.sprite = walkSprites[0]; // İlk frame'e sabitler (Idle gibi)
        }
    }
}
