using System.Collections;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Prefab mũi tên
    [SerializeField] private Transform firePoint;    // Vị trí bắn mũi tên
    [SerializeField] private float fireRate = 2f;    // Thời gian giữa mỗi lần bắn

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            FireBullet();
        }
    }

    private void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Bullet Prefab hoặc Fire Point chưa được gán!");
            return;
        }

        // Tạo mũi tên và đặt hướng bắn sang phải
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(Vector2.right); // Luôn bắn sang phải
    }
}
