using UnityEngine;
using DG.Tweening;

public class CharacterControllerDOTween : MonoBehaviour
{
    [Header("Sprite Renderer")]
    public SpriteRenderer spriteRenderer;

    [Header("Idle Animation")]
    public Sprite[] idleSprites;
    public float idleFrameRate = 0.2f;

    [Header("Walk Animation")]
    public Sprite[] walkSprites;
    public float walkFrameRate = 0.1f;

    [Header("Jump Animation")]
    public Sprite[] jumpSprites;
    public float jumpFrameRate = 0.1f;
    public float jumpHeight = 1f;
    public float jumpDuration = 0.5f;

    [Header("Attack Animation")]
    public Sprite[] attackSprites;
    public float attackFrameRate = 0.08f;
    public float attackCooldown = 0.5f; // Saldırı yaptıktan sonra bu kadar bekle

    private Sequence currentSequence;
    private bool isBusy = false;
    private bool canAttack = true;
    private int currentFrame = 0;
    private Vector2 input;

    void Start()
    {
        PlayIdle();
    }

    void Update()
    {
        if (isBusy) return; // Eğer bir animasyon oynuyorsa input alma

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayJump();
        }
        else if (Input.GetMouseButtonDown(0) && canAttack)
        {
            PlayAttack();
        }
        else if (input != Vector2.zero)
        {
            PlayWalk();
        }
        else
        {
            PlayIdle();
        }
    }

    void PlayIdle()
    {
        if (currentSequence != null) currentSequence.Kill();
        currentFrame = 0;

        currentSequence = DOTween.Sequence();

        for (int i = 0; i < idleSprites.Length; i++)
        {
            currentSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = idleSprites[currentFrame];
                currentFrame = (currentFrame + 1) % idleSprites.Length;
            });
            currentSequence.AppendInterval(idleFrameRate);
        }

        currentSequence.SetLoops(-1);
    }

    void PlayWalk()
    {
        if (currentSequence != null) currentSequence.Kill();
        currentFrame = 0;

        currentSequence = DOTween.Sequence();

        for (int i = 0; i < walkSprites.Length; i++)
        {
            currentSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = walkSprites[currentFrame];
                currentFrame = (currentFrame + 1) % walkSprites.Length;
            });
            currentSequence.AppendInterval(walkFrameRate);
        }

        currentSequence.SetLoops(-1);
    }

    void PlayJump()
    {
        if (currentSequence != null) currentSequence.Kill();
        isBusy = true;
        currentFrame = 0;

        Sequence jumpSequence = DOTween.Sequence();

        // Sprite değişimi
        for (int i = 0; i < jumpSprites.Length; i++)
        {
            jumpSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = jumpSprites[currentFrame];
                currentFrame = (currentFrame + 1) % jumpSprites.Length;
            });
            jumpSequence.AppendInterval(jumpFrameRate);
        }

        // Zıplama hareketi
        float originalY = transform.position.y;
        jumpSequence.Insert(0f, transform.DOMoveY(originalY + jumpHeight, jumpDuration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOMoveY(originalY, jumpDuration / 2).SetEase(Ease.InQuad);
            }));

        jumpSequence.OnComplete(() =>
        {
            isBusy = false;
        });
    }

    void PlayAttack()
    {
        if (currentSequence != null) currentSequence.Kill();
        isBusy = true;
        canAttack = false;
        currentFrame = 0;

        currentSequence = DOTween.Sequence();

        for (int i = 0; i < attackSprites.Length; i++)
        {
            currentSequence.AppendCallback(() =>
            {
                spriteRenderer.sprite = attackSprites[currentFrame];
                currentFrame = (currentFrame + 1) % attackSprites.Length;
            });
            currentSequence.AppendInterval(attackFrameRate);
        }

        currentSequence.OnComplete(() =>
        {
            isBusy = false;
            DOVirtual.DelayedCall(attackCooldown, () =>
            {
                canAttack = true;
            });
        });
    }
}
