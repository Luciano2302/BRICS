using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;
    public Text finalScoreText;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitToMenu);
        
        if (GameManager.Instance != null)
        {
            finalScoreText.text = "Final Score: " + GameManager.Instance.currentScore;
        }
    }

    void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void QuitToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitToMenu();
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    
}