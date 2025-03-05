using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private static int currentLevel = 1; // Biến static để giữ level

    private void Start()
    {
        // Cập nhật `currentLevel` dựa trên tên scene
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level 1") currentLevel = 1;
        else if (sceneName == "Level 2") currentLevel = 2;
        else if (sceneName == "Level 3") currentLevel = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentLevel == 1)
            {
                SceneManager.LoadScene("Level 2");
                currentLevel = 2;
            }
            else if (currentLevel == 2)
            {
                SceneManager.LoadScene("Level 3");
                currentLevel = 3;
            }
            else if (currentLevel == 3)
            {
                FindObjectOfType<GameManager>().GameOver(true);
            }
        }
    }
}
