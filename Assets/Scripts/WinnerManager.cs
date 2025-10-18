using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerManager : MonoBehaviour
{
    [Header("UI References")]
    public Button playAgainButton;
    public Button quitButton;
    public Text finalScoreText;
    
    void Start()
    {
        Debug.Log("WinnerManager Start chamado");
        
        // Busca automática das referências de UI
        FindUIReferences();
        
        // Configura os botões
        SetupButtons();
        
        // Atualiza o score final
        UpdateFinalScore();
    }
    
    // Encontra automaticamente os componentes de UI na cena
    void FindUIReferences()
    {
        // Busca pelo botão Play Again
        if (playAgainButton == null)
        {
            GameObject playAgainObj = GameObject.Find("PlayAgainButton");
            if (playAgainObj == null) playAgainObj = GameObject.Find("ButtonPlayAgain");
            if (playAgainObj == null) playAgainObj = GameObject.Find("BtnPlayAgain");
            
            if (playAgainObj != null) 
            {
                playAgainButton = playAgainObj.GetComponent<Button>();
                Debug.Log("Play Again button encontrado automaticamente: " + playAgainObj.name);
            }
        }
        
        // Busca pelo botão Quit
        if (quitButton == null)
        {
            GameObject quitObj = GameObject.Find("QuitButton");
            if (quitObj == null) quitObj = GameObject.Find("ButtonQuit");
            if (quitObj == null) quitObj = GameObject.Find("BtnQuit");
            
            if (quitObj != null) 
            {
                quitButton = quitObj.GetComponent<Button>();
                Debug.Log("Quit button encontrado automaticamente: " + quitObj.name);
            }
        }
        
        // Busca pelo texto do score final
        if (finalScoreText == null)
        {
            GameObject scoreObj = GameObject.Find("FinalScoreText");
            if (scoreObj == null) scoreObj = GameObject.Find("TextFinalScore");
            if (scoreObj == null) scoreObj = GameObject.Find("TxtFinalScore");
            
            if (scoreObj != null) 
            {
                finalScoreText = scoreObj.GetComponent<Text>();
                Debug.Log("Final Score text encontrado automaticamente: " + scoreObj.name);
            }
        }

        // Fallback: busca por qualquer botão se ainda não encontrou
        Button[] allButtons = FindObjectsOfType<Button>();
        if (playAgainButton == null && allButtons.Length >= 1)
        {
            playAgainButton = allButtons[0];
            Debug.Log("Usando primeiro botão disponível como Play Again: " + playAgainButton.name);
        }
        
        if (quitButton == null && allButtons.Length >= 2)
        {
            quitButton = allButtons[1];
            Debug.Log("Usando segundo botão disponível como Quit: " + quitButton.name);
        }
    }
    
    // Configura os listeners dos botões
    void SetupButtons()
    {
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgain);
            Debug.Log("Play Again button configurado");
        }
        else
        {
            Debug.LogError("Play Again Button não encontrado!");
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
            Debug.Log("Quit button configurado");
        }
        else
        {
            Debug.LogError("Quit Button não encontrado!");
        }
    }
    
    // Atualiza o texto do score final
    void UpdateFinalScore()
    {
        if (finalScoreText != null)
        {
            if (GameManager.Instance != null)
            {
                int finalScore = GameManager.Instance.GetFinalScore();
                finalScoreText.text = "Final Score: " + finalScore;
                Debug.Log("Score final atualizado: " + finalScore);
            }
            else
            {
                finalScoreText.text = "Final Score: 0";
                Debug.Log("Score final definido como 0 (GameManager não encontrado)");
            }
        }
        else
        {
            Debug.LogError("Final Score Text não encontrado!");
        }
    }
    
    // Reinicia o jogo
    void PlayAgain()
    {
        Debug.Log("Play Again clicado");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    
    // Sai do jogo
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