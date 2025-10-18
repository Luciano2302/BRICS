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
    private int bricksDestroyed = 0;
    private int initialBrickCount = 0;
    
    [Header("UI References")]
    public Text scoreText;
    public Text livesText;
    public Text levelText;
    
    [Header("Audio")]
    public AudioClip brickDestroySound;
    public AudioClip playerCollisionSound;
    public AudioClip levelCompleteSound;
    public AudioClip gameOverSound;
    public AudioClip victorySound;
    public AudioClip backgroundMusic;
    
    private AudioSource audioSource;
    private bool isTransitioning = false;

    void Awake()
    {
        // Implementação do Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager Instance criada e persistente!");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Configura o componente de audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Configura evento para quando cenas são carregadas
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Chamado quando uma cena é carregada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada: " + scene.name);
        
        // Reseta flags e contadores
        isTransitioning = false;
        bricksDestroyed = 0;
        
        // Reinicializa componentes da cena
        FindUIReferences();
        CountBricks();
        UpdateUI();
        
       if (AudioManager.Instance != null)
    {
        if (scene.name != "MenuScene" && scene.name != "GameOverScene" && scene.name != "WinnerScene")
        {
            AudioManager.Instance.PlayBackgroundMusic();
        }
        else
        {
            AudioManager.Instance.StopBackgroundMusic();
        }
    }
        
        Debug.Log("GameManager reinicializado para cena: " + scene.name);
        Debug.Log("Level: " + currentLevel + " | Score: " + currentScore + " | Lives: " + playerLives);
    }
    
    // Encontra referências de UI automaticamente na cena
    void FindUIReferences()
    {
        Text[] allTexts = FindObjectsOfType<Text>();
        foreach (Text text in allTexts)
        {
            if (text.name == "TextScore") scoreText = text;
            if (text.name == "TextLives") livesText = text;
            if (text.name == "TextLevel") levelText = text;
        }
        
        Debug.Log("Referências de UI encontradas: " + 
                 (scoreText != null) + " | " + 
                 (livesText != null) + " | " + 
                 (levelText != null));
    }
    
    void Start()
    {
        Debug.Log("GameManager Start chamado");
    }
    
    // Adiciona pontos ao score
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateUI();
    }
    
    // Remove uma vida do jogador
    public void LoseLife()
    {
        playerLives--;
        UpdateUI();
        
        if (playerLives <= 0)
        {
            GameOver();
        }
    }
    
    // Chamado quando um brick é destruído
    public void BrickDestroyed()
    {
        // Ignora se estiver em transição entre cenas
        if (isTransitioning) 
        {
            Debug.Log("Ignorando brick destruído durante transição...");
            return;
        }
        
        bricksDestroyed++;
        AddScore(10);
        
        if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayBrickDestroy();
    }
        
        Debug.Log("=== BRICK DESTRUÍDO ===");
        Debug.Log("Bricks destruídos: " + bricksDestroyed + "/" + initialBrickCount);
        
        // Verifica se todos os bricks foram destruídos
        if (bricksDestroyed >= initialBrickCount) 
        {
            Debug.Log("🎉🎉🎉 TODOS OS BRICKS DESTRUÍDOS! Level Complete!");
            LevelComplete();
        }
        else
        {
            Debug.Log("Ainda faltam " + (initialBrickCount - bricksDestroyed) + " bricks para completar o nível");
        }
    }

    // Conta todos os bricks na cena
    void CountBricks()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Enemy");
        totalBricks = bricks.Length;
        initialBrickCount = totalBricks;
        
        // Verificação adicional para garantir que não há bricks com health <= 0
        foreach (GameObject brick in bricks)
        {
            BrickModel brickModel = brick.GetComponent<BrickModel>();
            if (brickModel != null && brickModel.Health <= 0)
            {
                Debug.LogWarning("Brick com health <= 0 encontrado na inicialização: " + brick.name);
                Destroy(brick);
            }
        }
        
        // Re-conta após a limpeza
        totalBricks = GameObject.FindGameObjectsWithTag("Enemy").Length;
        initialBrickCount = totalBricks;
        
        Debug.Log("Total de bricks na cena: " + totalBricks);
    }
    
    // Chamado quando a bola colide com o player
    public void PlayerCollision()
    {
        AddScore(-5);
        if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayPlayerCollision();
    }
    }
    
    // Completa o nível atual
    void LevelComplete()
    {
        isTransitioning = true;
        
        Debug.Log("=== INICIANDO LEVELCOMPLETE ===");
        AddScore(100);
        if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayLevelComplete();
    }
        currentLevel++;
        
        Debug.Log("Novo nível: " + currentLevel);
        
        // Verifica se é o último nível
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
    
    // Carrega o próximo nível
    void LoadNextLevel()
    {
        string sceneName = "Level" + currentLevel;
        Debug.Log("Tentando carregar cena: " + sceneName);
        
        // Restaura vidas para o próximo nível
        playerLives = 3;
        Debug.Log("Vidas restauradas para: " + playerLives);
        
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
    
    // Game Over
    void GameOver()
    {
        isTransitioning = true;
        if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayGameOver();
        AudioManager.Instance.StopBackgroundMusic();
    }
        
        Debug.Log("GAME OVER - Resetando estado do jogo");
        currentScore = 0;
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("GameOverScene");
    }
    
    // Vitória - Todos os níveis completos
    void WinGame()
    {
        isTransitioning = true;
      if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayVictory();
        AudioManager.Instance.StopBackgroundMusic();
    }
        
        Debug.Log("VITÓRIA - Resetando estado do jogo");
        // Mantém o score final para exibir na tela de vitória
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("WinnerScene");
    }
    
    // Atualiza a interface do usuário
    void UpdateUI()
    {
        if (scoreText != null) 
        {
            scoreText.text = "Score: " + currentScore;
            Debug.Log("UI Score atualizado: " + currentScore);
        }
        if (livesText != null) livesText.text = "Lives: " + playerLives;
        if (levelText != null) levelText.text = "Level: " + currentLevel;
    }
    
    // Métodos de Audio
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    
    void StopBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    
    // Reinicia o jogo completamente
    public void RestartGame()
    {
        Debug.Log("RESTART GAME - Reset completo");
        currentScore = 0;
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("MainScene");
    }
    
    // Volta para o menu
    public void QuitToMenu()
    {
        Debug.Log("QUIT TO MENU - Reset completo");
        currentScore = 0;
        playerLives = 3;
        currentLevel = 1;
        SceneManager.LoadScene("MenuScene");
    }
    
    // Sai do jogo
    public void QuitGame()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    // Getter para o score final (usado na tela de vitória)
    public int GetFinalScore()
    {
        return currentScore;
    }
    
    // Limpa evento ao destruir
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}