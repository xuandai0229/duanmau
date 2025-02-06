using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức Respawn của PlayerMovement
            other.GetComponent<PlayerMovement>()?.Respawn();
        }
    }
}
