using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public GameObject ClosedChest;
    public GameObject OpenedChest;

    public GameObject portal;

    void OnTriggerEnter2D(Collider2D other)
    {
        ClosedChest.SetActive(false);
        OpenedChest.SetActive(true);
        portal.SetActive(true);
        
    }
}
