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
    private int totalBricks = 0;
    
    [Header("UI References")]
    public Text scoreText;
    public Text livesText;
    public Text levelText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager Instance criada!");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        CountBricks();
        UpdateUI();
        Debug.Log("GameManager iniciado! Bricks: " + totalBricks);
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
    }
    
    public void BrickDestroyed()
    {
        // ⭐⭐ DEBUG DETALHADO
        int realBricks = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalBricks = realBricks;
        
        AddScore(10);
        
        Debug.Log("=== BRICK DESTRUÍDO ===");
        Debug.Log("Bricks reais na cena: " + realBricks);
        Debug.Log("Score: " + currentScore);
        Debug.Log("Current Level: " + currentLevel);
        
        // ⭐⭐ LISTA TODOS OS BRICKS RESTANTES
        GameObject[] remainingBricks = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("=== BRICKS RESTANTES ===");
        foreach(GameObject brick in remainingBricks)
        {
            Debug.Log("Brick: " + brick.name + " ativo: " + brick.activeInHierarchy);
        }
        Debug.Log("=== FIM DA LISTA ===");
        
        if (realBricks <= 0) 
        {
            Debug.Log("🎉🎉🎉 TODOS OS BRICKS DESTRUÍDOS! Level Complete!");
            LevelComplete();
        }
        else
        {
            Debug.Log("Ainda faltam " + realBricks + " bricks para completar o nível");
        }
    }

    void CountBricks()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Total de bricks inicial: " + totalBricks);
    }
    
    public void PlayerCollision()
    {
        AddScore(-5);
    }
    
    void LevelComplete()
    {
        Debug.Log("=== INICIANDO LEVELCOMPLETE ===");
        AddScore(100);
        currentLevel++;
        
        Debug.Log("Novo nível: " + currentLevel);
        
        if (currentLevel > 2) 
        {
            Debug.Log("VITÓRIA - Todas as fases completas!");
            WinGame();
        }
        else
        {
            Debug.Log("Carregando próxima fase: Level" + currentLevel);
            LoadNextLevel();
        }
    }
    
    void LoadNextLevel()
    {
        string sceneName = "Level" + currentLevel;
        Debug.Log("Tentando carregar cena: " + sceneName);
        
        // ⭐ VERIFICA se a cena existe
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("CENA NÃO ENCONTRADA: " + sceneName);
            Debug.Log("Indo para vitória como fallback...");
            WinGame();
        }
    }
    
    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    
    void WinGame()
    {
        SceneManager.LoadScene("WinnerScene");
    }
    
    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
        if (livesText != null) livesText.text = "Lives: " + playerLives;
        if (levelText != null) levelText.text = "Level: " + currentLevel;
    }
    
    public void RestartGame()
    {
        currentScore = 0;
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("MainScene");
    }
    
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}