using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game State")]
    public int currentScore = 0;
    public int playerLives = 3;
    public int currentLevel = 1;
    public int bricksCount = 0;
    
    [Header("UI References")]
    public Text scoreText;
    public Text livesText;
    public Text levelText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        CountBricks();
        UpdateUI();
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();
    }
    
    public void LoseLife()
    {
        playerLives--;
        UpdateUI();
        
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Recriar bola na próxima implementação
        }
    }
    
    public void BrickDestroyed()
    {
        bricksCount--;
        AddScore(10); // Pontos por destruir tijolo
        
        if (bricksCount <= 0)
        {
            AddScore(100); // Bônus por completar fase
            LoadNextLevel();
        }
    }
    
    void CountBricks()
    {
        bricksCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    
    public void LoadNextLevel()
    {
        currentLevel++;
        // Por enquanto, recarregar a mesma cena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CountBricks();
    }
    
    void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + currentScore;
        if (livesText) livesText.text = "Lives: " + playerLives;
        if (levelText) levelText.text = "Level: " + currentLevel;
    }
    
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    public void RestartGame()
    {
        currentScore = 0;
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}