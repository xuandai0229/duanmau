using UnityEngine;
using UnityEngine.SceneManagement;  // Để chuyển đổi scene
using TMPro;  // Để sử dụng TextMeshPro

public class MainMenuController : MonoBehaviour
{
    // Hàm bắt đầu game
    public void StartGame()
    {
        // Chuyển sang scene Level1
        SceneManager.LoadScene("Lever 1");
    }

    // Hàm thoát game
    public void ExitGame()
    {
        // Thoát game khi chơi ở chế độ build
        Debug.Log("Exiting Game");
        Application.Quit();  // Thoát game
    }
}
