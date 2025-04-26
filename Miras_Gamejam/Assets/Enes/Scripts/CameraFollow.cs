using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edece�imiz karakter
    public float smoothSpeed = 5f; // Takip yumu�akl���

    private float fixedY; // Sabitlenecek Y pozisyonu
    private float fixedZ; // Sabitlenecek Z pozisyonu (kamera geri planda kalmal�)

    private void Start()
    {
        // Ba�lang��ta kameran�n Y ve Z pozisyonunu kaydediyoruz
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Sadece hedefin X pozisyonunu al, Y ve Z sabit kals�n
        Vector3 desiredPosition = new Vector3(target.position.x, fixedY, fixedZ);

        // Kameray� yumu�ak bir �ekilde takip ettir
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
