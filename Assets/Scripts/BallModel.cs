using UnityEngine;

public class BallModel : MonoBehaviour
{

    [SerializeField]private Vector2 _direction;
    [SerializeField]private float _speed;
    [SerializeField]private float _power;

    public float Power {get => _power; set => _power = value;}
    public float Speed {get => _speed; set => _speed = value;}
    public Vector2 Direction {get => _direction; set => _direction = value;}
}
