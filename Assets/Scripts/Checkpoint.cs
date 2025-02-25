using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu Player chạm vào checkpoint
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.UpdateCheckpoint(transform.position);
                Debug.Log("Checkpoint updated: " + transform.position);
            }
        }
    }
}

