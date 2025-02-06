using UnityEngine;

public class SpringJump : MonoBehaviour
{
    public float jumpForce = 20f; // Điều chỉnh độ cao của cú nhảy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra nếu chạm vào Player
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vận tốc Y để không cộng dồn
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Đẩy Player lên
            }
        }
    }
}
