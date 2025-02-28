using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // Singleton

    public GameObject winPanel, losePanel;
    public TMP_Text coinText, winCoinText, loseCoinText;

    private int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Giữ lại khi chuyển Scene
        }
        else
        {
            Destroy(gameObject);  // Nếu đã có một GameManager, hủy cái mới
        }
    }

    private void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "💰 Coins: " + coins;
    }

    public void GameOver(bool isWin)
    {
        if (isWin && winPanel != null)
        {
            winPanel.SetActive(true);  // Hiển thị panel chiến thắng
            winCoinText.text = "🎉 Bạn đã thắng!\nSố Coin: " + coins;
        }
        else if (!isWin && losePanel != null)
        {
            losePanel.SetActive(true);
            loseCoinText.text = "❌ Bạn đã thua!\nSố Coin: " + coins;
        }

        Invoke("PauseGame", 0.1f);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;  // Dừng thời gian khi game kết thúc
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Debug.Log("Thoát game!");
        Application.Quit();
    }
}
