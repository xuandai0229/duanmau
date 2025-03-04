﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Chỉ tác động lên enemy
        {
            Destroy(other.gameObject); // Tiêu diệt enemy khi trúng đạn
            Destroy(gameObject); // Hủy viên đạn
        }
    }
}
