using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    public Transform characterTransform;
    public SpriteRenderer swordRenderer;
    public Sprite[] attackSprites; // 0: normal, 1: vurma frame1, 2: vurma frame2, vs...

    private bool canAttack = true;
    public float attackCooldown = 0.5f; // Saldırı sonrası bekleme süresi

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        canAttack = false; // Saldırı yapınca yeni saldırıyı hemen engelle

        // Kılıç savurma animasyonu (sprite değişimi)
        Sequence attackSequence = DOTween.Sequence();

        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[0]);
        attackSequence.AppendInterval(0.1f);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[1]);
        attackSequence.AppendInterval(0.1f);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[2]);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[3]);
        attackSequence.AppendInterval(0.1f);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[4]);
        attackSequence.AppendInterval(0.1f);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[5]);
        attackSequence.AppendCallback(() => swordRenderer.sprite = attackSprites[6]);
        attackSequence.AppendInterval(0.1f);

        // Saldırı animasyonu bittikten sonra cooldownu başlat
        attackSequence.OnComplete(() =>
        {
            Invoke(nameof(ResetAttack), attackCooldown);
        });
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
