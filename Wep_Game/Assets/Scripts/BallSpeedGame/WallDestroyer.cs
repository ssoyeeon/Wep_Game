using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(collision.gameObject);
        }
    }
}