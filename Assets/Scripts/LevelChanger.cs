using UnityEngine;
using UnityEngine.SceneManagement;  // Để chuyển đổi scene

public class LevelChanger : MonoBehaviour
{
    private static int currentLevel = 1;  // Biến để theo dõi level hiện tại

    // Hàm gọi khi người chơi va chạm với vật thể
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm là "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Kiểm tra và chuyển đến Level tiếp theo
            if (currentLevel == 1)
            {
                SceneManager.LoadScene("Level 2");
                currentLevel = 2;  // Cập nhật level hiện tại
            }
            else if (currentLevel == 2)
            {
                SceneManager.LoadScene("Level 3");
                currentLevel = 3;  // Cập nhật level hiện tại
            }
        }
    }
}
