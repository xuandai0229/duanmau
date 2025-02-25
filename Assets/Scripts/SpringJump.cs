using UnityEngine;

public class SpringJump : MonoBehaviour
{
    public float jumpForce = 50f; // Điều chỉnh độ cao của cú nhảy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra nếu chạm vào Player
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Giữ lại vận tốc Y nếu đang bay lên, tránh reset làm mất đà
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0f));
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Đặt lại vận tốc để nhảy cao hơn
            }
        }
    }
}
