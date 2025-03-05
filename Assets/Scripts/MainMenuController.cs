using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text leaderboardText, welcomeText;

    private void Start()
    {
        string username = PlayerPrefs.GetString("SavedUsername", "Guest");
        welcomeText.text = " Chào " + username + "!";

        ShowLeaderboard();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey("SavedUsername");
        SceneManager.LoadScene("LoginScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ShowLeaderboard()
    {
        leaderboardText.text = " Top 3 Thành Tích:\n1. Chưa có dữ liệu\n2. Chưa có dữ liệu\n3. Chưa có dữ liệu";
    }
}
