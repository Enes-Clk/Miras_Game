using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edeceðimiz karakter
    public float smoothSpeed = 5f; // Takip yumuþaklýðý

    private float fixedY; // Sabitlenecek Y pozisyonu
    private float fixedZ; // Sabitlenecek Z pozisyonu (kamera geri planda kalmalý)

    private void Start()
    {
        // Baþlangýçta kameranýn Y ve Z pozisyonunu kaydediyoruz
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Sadece hedefin X pozisyonunu al, Y ve Z sabit kalsýn
        Vector3 desiredPosition = new Vector3(target.position.x, fixedY, fixedZ);

        // Kamerayý yumuþak bir þekilde takip ettir
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
