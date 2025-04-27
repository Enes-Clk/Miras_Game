using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    

    public void StartGame()
    {
        // Buraya kendi oyun sahnenin ismini yaz!
        SceneManager.LoadScene("OyunSahnesi");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatılıyor...");
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }


}
