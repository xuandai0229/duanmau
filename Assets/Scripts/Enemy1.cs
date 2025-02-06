using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float moveDistance = 1f; // Khoảng cách di chuyển theo chiều ngang
    public float moveSpeed = 2f;     // Tốc độ di chuyển

    private Vector3 initialPosition;

    void Start()
    {
        // Lưu vị trí ban đầu của quái vật
        initialPosition = transform.position;
    }

    void Update()
    {
        // Di chuyển qua lại
        float moveDirection = Mathf.Sin(Time.time * moveSpeed);
        transform.position = new Vector3(initialPosition.x + moveDirection * moveDistance, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Gọi phương thức Respawn của PlayerMovement
            collision.gameObject.GetComponent<PlayerMovement>()?.Respawn();
        }
    }
}
