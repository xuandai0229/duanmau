using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput, passwordInput;
    public TMP_Text messageText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedUsername"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (PlayerPrefs.HasKey(username))
        {
            string savedPassword = PlayerPrefs.GetString(username);
            if (savedPassword == password)
            {
                PlayerPrefs.SetString("SavedUsername", username);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                messageText.text = "❌ Sai mật khẩu!";
            }
        }
        else
        {
            messageText.text = "❌ Tài khoản không tồn tại!";
        }
    }

    public void Register()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (PlayerPrefs.HasKey(username))
        {
            messageText.text = "⚠️ Tên đã tồn tại!";
        }
        else
        {
            PlayerPrefs.SetString(username, password);
            messageText.text = "✅ Đăng ký thành công!";
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
