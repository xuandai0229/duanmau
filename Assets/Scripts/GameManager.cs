using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public GameObject gameMenu;

    private bool isGameOver = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinGame()
    {
        isGameOver = true;
        winText.gameObject.SetActive(true);
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        PlayerPrefs.DeleteKey("PlayerLives"); // Reset mạng
        PlayerPrefs.DeleteKey("PlayerCoins"); // Reset coins về 0 khi restart game
        SceneManager.LoadScene("MainMenu");
    }
}
