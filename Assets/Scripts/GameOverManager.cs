using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;
    public Button quitButton;
    public Button quitGameButton;
    public Text finalScoreText;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitToMenu);
        quitGameButton.onClick.AddListener(QuitGame);
        
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

      void QuitGame()
    {
        Debug.Log("Quit clicado");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
    
}