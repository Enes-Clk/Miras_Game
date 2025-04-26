// Karakterin klavye ile hareket etmesini sağlar (2D oyunlar için)
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(moveX, moveY) * speed * Time.deltaTime;
    }
}