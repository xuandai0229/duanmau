using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Tốc độ đạn
    [SerializeField] private float lifeTime = 3f; // Thời gian tồn tại của đạn

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime); // Tự hủy sau lifeTime giây
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Di chuyển đạn
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Va chạm với kẻ thù
        {
            Destroy(collision.gameObject); // Hủy kẻ thù
            Destroy(gameObject);           // Hủy viên đạn
        }
    }
}
