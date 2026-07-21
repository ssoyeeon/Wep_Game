using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public GameObject DoneCanvas;
    private void Awake()
    {
        DoneCanvas.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            DoneCanvas.SetActive(true);
        }
    }
}