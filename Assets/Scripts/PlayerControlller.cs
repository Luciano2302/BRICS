using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    private PlayerModel _playerModel;
    private Transform _transform;



    void Start()
    {
        _playerModel = GetComponent<PlayerModel>();
        _transform   = GetComponent<Transform>();
    }

    public void Move(float h)
    {
        if((_transform.position.x > -1.419f && h < 0f) || 
           (_transform.position.x <  1.435f && h > 0f)){
        _transform.Translate( h * _playerModel.Speed , 0f , 0f );   
    }
    }
}
