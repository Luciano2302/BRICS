using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerControlller _playerController;

    void Start()
    {
        _playerController = GetComponent<PlayerControlller>();
        Debug.Log("PlayerView iniciado");
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); 
        
        //DEBUG DO INPUT
        if (h != 0 && Time.frameCount % 30 == 0)
        {
            Debug.Log($"Input Horizontal: {h:F2}");
        }
        
        _playerController.Move(h);
    }
}