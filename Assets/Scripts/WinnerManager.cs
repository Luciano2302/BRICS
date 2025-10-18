using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerManager : MonoBehaviour
{
    public Button quitButton;
    public Text finalScoreText;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
        
        if (GameManager.Instance != null)
        {
            finalScoreText.text = "You Win! Score: " + GameManager.Instance.currentScore;
        }
    }

    void QuitGame()
    {
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