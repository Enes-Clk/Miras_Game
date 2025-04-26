using DG.Tweening;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public SpriteRenderer swordRenderer;
    public Sprite[] attackSprites; // 0: normal, 1: savurma frame1, 2: savurma frame2...

    private void Start()
    {
        // Örnek: kılıcı savur
        SwordSwing();
    }

    public void SwordSwing()
    {
        Sequence swingSequence = DOTween.Sequence();

        
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[0]);
        swingSequence.AppendInterval(0.1f); // 0.1 saniye sonra
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[1]);
        swingSequence.AppendInterval(0.1f); // 0.1 saniye sonra
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[2]);
        swingSequence.AppendInterval(0.1f);
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[3]);

        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[4]);
        swingSequence.AppendInterval(0.1f); // 0.1 saniye sonra
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[5]);
        swingSequence.AppendInterval(0.1f);
        swingSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[6]);
    }
}
