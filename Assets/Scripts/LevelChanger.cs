using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private static int currentLevel = 1;
    public GameManager gameManager;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
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
                gameManager.WinGame();
            }
        }
    }
}
