using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrowPrefab;   // Prefab mũi tên
    public Transform firePoint;      // Điểm bắn mũi tên
    public float fireRate = 2f;      // Tốc độ bắn (có thể thay đổi để mỗi bẫy có thời gian khác nhau)
    public float firstShotDelay = 0f; // Thời gian trễ trước khi bắn lần đầu

    void Start()
    {
        InvokeRepeating("ShootArrow", firstShotDelay, fireRate);
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetDirection(Vector2.right); // Mũi tên bay sang phải
    }
}
