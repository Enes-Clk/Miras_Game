using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    public Transform characterTransform;
    public SpriteRenderer swordRenderer;
    public Sprite[] normalAttackSprites; // Normal vuruş animasyonu kareleri
    public Sprite[] reverseAttackSprites; // Ters vuruş animasyonu kareleri

    private bool canAttack = true;
    private bool isNextAttackReversed = false; // Sıradaki vuruş ters mi?

    public float attackCooldown = 0.5f; // Her saldırı sonrası bekleme süresi
    private bool isHoldingMouse = false; // Farenin basılı tutulduğunu algılamak için

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            isHoldingMouse = true;
            SingleAttack(); // İlk tıklamada normal bir vuruş
        }

        if (Input.GetMouseButton(0) && canAttack && isHoldingMouse)
        {
            HoldAttack(); // Basılı tutuluyorsa düz-ters saldırıları başlat
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHoldingMouse = false; // Mouse bırakılınca durdur
        }
    }

    void SingleAttack()
    {
        canAttack = false;

        Sequence attackSequence = DOTween.Sequence();

        float frameInterval = 0.1f;

        for (int i = 0; i < normalAttackSprites.Length; i++)
        {
            attackSequence.AppendCallback(() => swordRenderer.sprite = normalAttackSprites[i]);
            attackSequence.AppendInterval(frameInterval);
        }

        attackSequence.OnComplete(() =>
        {
            Invoke(nameof(ResetAttack), attackCooldown);
        });
    }

    void HoldAttack()
    {
        canAttack = false;

        Sprite[] currentAttackSprites = isNextAttackReversed ? reverseAttackSprites : normalAttackSprites;

        Sequence attackSequence = DOTween.Sequence();

        float frameInterval = 0.1f;

        for (int i = 0; i < currentAttackSprites.Length; i++)
        {
            attackSequence.AppendCallback(() => swordRenderer.sprite = currentAttackSprites[i]);
            attackSequence.AppendInterval(frameInterval);
        }

        attackSequence.OnComplete(() =>
        {
            isNextAttackReversed = !isNextAttackReversed; // Sonraki saldırıyı ters yap
            Invoke(nameof(ResetAttack), attackCooldown);
        });
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
