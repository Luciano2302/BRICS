using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button startButton;
    public Text gameTitle;
    public Text objectiveText;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        
        gameTitle.text = "BRICK BREAKER";
        objectiveText.text = "Objetivo: Quebre todos os tijolos usando a bola!\nEvite que a bola caia para baixo do player.\n\nControles: Use as setas ← → para mover";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}