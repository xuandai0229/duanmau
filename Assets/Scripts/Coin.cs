using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức CollectCoin của PlayerMovement
            other.GetComponent<PlayerMovement>()?.CollectCoin();
            Destroy(gameObject); // Xóa coin khỏi màn chơi
        }
    }
}
