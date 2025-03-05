using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu người chơi chạm vào
        {
            Debug.Log("🎉 Người chơi đã chạm vào WinZone!");
            GameManager.Instance.GameOver(true); // Gọi hàm Win từ GameManager
        }
    }
}
