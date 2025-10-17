using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerControlller _playerController;

    void Start()
    {
        _playerController = GetComponent<PlayerControlller>();
    }

    void Update()
    {
       float h = Input.GetAxis("Horizontal"); 
       _playerController.Move(h);

    }
}
