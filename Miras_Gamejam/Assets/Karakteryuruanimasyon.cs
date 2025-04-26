using DG.Tweening;
using UnityEngine;

public class CharacterWalkAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    public Sprite[] walkSprites;          
    public float frameRate = 0.1f;         

    private int currentFrame = 0;
    private Sequence walkSequence;

    void Start()
    {
        PlayWalkAnimation();
    }

    void PlayWalkAnimation()
    {
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

        walkSequence.SetLoops(-1); 
    }
}

