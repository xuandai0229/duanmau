using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Respawn();  // Gọi hàm Respawn() khi trúng Player
            Destroy(gameObject);  // Xóa mũi tên
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);  // Xóa mũi tên khi trúng tường
        }
    }
}
