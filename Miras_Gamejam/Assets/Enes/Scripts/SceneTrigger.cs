using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTrigger : MonoBehaviour
{
    public GameObject Text; // Ekranda gösterilecek yazı
    // public string nextSceneName; // Geçiş yapılacak sahnenin adı
    private bool isPlayerInTrigger = false; // Player trigger içinde mi?

    [SerializeField] string sceneName;

    void Start()
    {
        Text.gameObject.SetActive(false); // Başlangıçta yazıyı gizle
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Eğer player triggera girerse
        {
            isPlayerInTrigger = true;
            Text.gameObject.SetActive(true); // Yazıyı göster
           // promptText.text = "Sahneye geçmek için 'E' tuşuna basın"; // Mesajı değiştir
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Eğer player triggerdan çıkarsa
        {
            isPlayerInTrigger = false;
            Text.gameObject.SetActive(false); // Yazıyı gizle
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E)) // Eğer player triggerda ve E tuşuna basılırsa
        {
            SceneManager.LoadScene(sceneName); // Belirtilen sahneye geç
        }
    }
}

