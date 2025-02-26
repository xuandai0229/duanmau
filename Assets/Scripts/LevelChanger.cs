using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private static int currentLevel = 1;  // Biến theo dõi cấp độ hiện tại

    private void Start()
    {
        // Không cần khởi tạo GameManager nữa
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Kiểm tra nếu đối tượng va chạm có tag "Player"
        {
            if (currentLevel == 1)
            {
                SceneManager.LoadScene("Level 2");  // Chuyển sang Level 2
                currentLevel = 2;
            }
            else if (currentLevel == 2)
            {
                SceneManager.LoadScene("Level 3");  // Chuyển sang Level 3
                currentLevel = 3;
            }
            else if (currentLevel == 3)
            {
                // Chỉ cần thông báo chiến thắng khi ở Level 3
                Debug.Log("Bạn đã chiến thắng game!");
                // Bạn có thể thêm đoạn mã để kết thúc game hoặc chuyển sang màn hình chiến thắng tại đây
            }
        }
    }
}
