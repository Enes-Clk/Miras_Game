using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTP : MonoBehaviour
{
    [SerializeField] string scene;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene);

        }
        
    }

}
