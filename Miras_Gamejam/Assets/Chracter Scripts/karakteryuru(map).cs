using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sideWalkSprites;  // Sol ve sağ yürüyüş animasyonu için sprite dizisi
    public Sprite[] upWalkSprites;    // Yukarı yürüyüş animasyonu için sprite dizisi
    public Sprite[] downWalkSprites;  // Aşağı yürüyüş animasyonu için sprite dizisi

    public float moveSpeed = 5f;  // Hareket hızı

    private Vector2 movementDirection;  // Hareket yönü
    private bool isMoving = false;  // Karakterin hareket edip etmediğini kontrol et

    void Update()
    {
        // Karakter hareketini al
        movementDirection.x = Input.GetAxisRaw("Horizontal");  // A-D tuşları (Sağa-Sola)
        movementDirection.y = Input.GetAxisRaw("Vertical");  // W-S tuşları (Yukarı-Aşağı)

        if (movementDirection != Vector2.zero)
        {
            // Eğer hareket varsa, animasyonu ve hareketi başlat
            MoveCharacter(movementDirection);
            AnimateCharacter(movementDirection);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        // Karakteri belirtilen yönde hareket ettir
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void AnimateCharacter(Vector2 direction)
    {
        if (direction.x != 0)  // Yalnızca sağ ve sol hareketi kontrol et
        {
            if (direction.x < 0)  // Eğer sola hareket ediyorsa
            {
                transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);  // Karakteri sola döndür
            }
            else if (direction.x > 0)  // Eğer sağa hareket ediyorsa
            {
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);  // Karakteri sağa döndür
            }
            
            PlaySideWalkAnimation();  // Sola ve sağa yürüyüş animasyonunu başlat
        }
        else if (direction.y > 0)  // Yukarı hareketi kontrol et
        {
            PlayUpWalkAnimation();  // Yukarı yürüyüş animasyonunu başlat
        }
        else if (direction.y < 0)  // Aşağı hareketi kontrol et
        {
            PlayDownWalkAnimation();  // Aşağı yürüyüş animasyonunu başlat
        }
    }

    void PlaySideWalkAnimation()
    {
        if (!isMoving)
        {
            isMoving = true;

            // DOTween ile animasyonu başlat
            Sequence walkSequence = DOTween.Sequence();

            foreach (var sprite in sideWalkSprites)
            {
                walkSequence.AppendCallback(() => spriteRenderer.sprite = sprite);
                walkSequence.AppendInterval(0.1f);  // Her sprite arasında bekleme süresi
            }

            walkSequence.OnKill(() => isMoving = false);  // Animasyon bitince hareketi durdur
        }
    }

    void PlayUpWalkAnimation()
    {
        if (!isMoving)
        {
            isMoving = true;

            // Yukarı hareketi için animasyon
            Sequence walkSequence = DOTween.Sequence();

            foreach (var sprite in upWalkSprites)
            {
                walkSequence.AppendCallback(() => spriteRenderer.sprite = sprite);
                walkSequence.AppendInterval(0.1f);  // Her sprite arasında bekleme süresi
            }

            walkSequence.OnKill(() => isMoving = false);  // Animasyon bitince hareketi durdur
        }
    }

    void PlayDownWalkAnimation()
    {
        if (!isMoving)
        {
            isMoving = true;

            // Aşağı hareketi için animasyon
            Sequence walkSequence = DOTween.Sequence();

            foreach (var sprite in downWalkSprites)
            {
                walkSequence.AppendCallback(() => spriteRenderer.sprite = sprite);
                walkSequence.AppendInterval(0.1f);  // Her sprite arasında bekleme süresi
            }

            walkSequence.OnKill(() => isMoving = false);  // Animasyon bitince hareketi durdur
        }
    }
}
