using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public GameObject stairPrefab; // Prefab bậc thang
    public int stairCount = 5; // Số lượng bậc thang muốn tạo
    public float stairSpacing = 3f; // Khoảng cách giữa các bậc thang
    public Vector2 startPosition = new Vector2(14.6f, 4.3f); // Vị trí bậc thang đầu tiên

    private void Start()
    {
        SpawnStairs();
    }

    void SpawnStairs()
    {
        for (int i = 0; i < stairCount; i++)
        {
            Vector3 spawnPosition = new Vector3(startPosition.x + i * stairSpacing, startPosition.y, 0);
            Instantiate(stairPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
