using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _life;
    [SerializeField] private float _scale = 1f;

    public float Speed { 
        get => _speed; 
        set => _speed = value; 
    }
    public float Life { get => _life; set => _life = value; }
    public float Scale { get => _scale; set => _scale = value; }
}