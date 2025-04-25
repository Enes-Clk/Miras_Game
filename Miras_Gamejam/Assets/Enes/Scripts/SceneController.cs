// Oyunu başlatmak, yeniden başlatmak veya çıkmak için sahne kontrolleri
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Sahne ismini yaz
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}