using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel, losePanel; // Panel hiển thị khi thắng/thua
    public TMP_Text coinText, winCoinText, loseCoinText; // Hiển thị số coin

    private int coins = 0;

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinText.text = "💰 Coins: " + coins;
    }

    public void GameOver(bool isWin)
    {
        if (isWin)
        {
            winPanel.SetActive(true);
            winCoinText.text = "Bạn đã thắng!\nSố Coin: " + coins;
        }
        else
        {
            losePanel.SetActive(true);
            loseCoinText.text = "Bạn đã thua!\nSố Coin: " + coins;
        }

        Time.timeScale = 0; // Dừng game
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
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
