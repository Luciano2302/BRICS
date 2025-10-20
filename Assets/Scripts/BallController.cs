using UnityEngine;

public class BallController : MonoBehaviour
{
    private BallModel _ballModel;
    private BallView _ballView;
    private Rigidbody2D _ballRigidbody;

    void Start()
    {
        _ballModel = GetComponent<BallModel>();
        _ballView = GetComponent<BallView>();
        _ballRigidbody = GetComponent<Rigidbody2D>();
        
        // Inicializa a bola
        AngleChange(_ballModel.Direction);
    }

    public void AngleChange(Vector2 dir)
    {
        _ballModel.Direction = dir;
        _ballRigidbody.velocity = dir * _ballModel.Speed;
    }

    // ⭐ NOVOS MÉTODOS MVC
    public void HandleBrickCollision(Collision2D collision)
    {
        // Destroi o brick
        BrickView brickView = collision.gameObject.GetComponent<BrickView>();
        if(brickView != null)
        {
            brickView.PerformTakeDamage(1f);
        }
        
        // Reflexão
        _ballView.ReflectBall(collision, _ballModel.Direction, _ballModel.Speed);
    }

    public void HandlePlayerCollision(Collision2D collision)
    {
        // Reflexão com ângulo calculado
        Vector2 newDirection = CalcBallAngleReflect(collision);
        AngleChange(newDirection);
        
        // Penalidade
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PlayerCollision();
        }
    }

    public void HandleGameOverCollision()
    {
        // Perde vida e reseta bola
        if(GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();
        }
        _ballView.ResetBall(new Vector3(0, -3, 0), new Vector2(0, 1), _ballModel.Speed);
    }

    public void HandleWallCollision(Collision2D collision)
    {
       //COMENTADO - O BallView agora cuida disso
    // NÃO FAZER NADA - o Rigidbody2D nativo + BallView resolvem
    
    Debug.Log("Wall collision handled by physics system");
    
    // Apenas debug
    if(_ballView != null)
    {
        _ballView.EnsureMinimumSpeed();
    }
    }

    public void PerfectAngleReflect(Collision2D collision)
    {
    Vector2 currentDirection = _ballRigidbody.velocity.normalized;
    Vector2 normal = collision.contacts[0].normal;
    Vector2 newDirection = Vector2.Reflect(currentDirection, normal);
    
    //Garante que a direção está normalizada
    newDirection = newDirection.normalized;
    
    AngleChange(newDirection);    }

    public Vector2 CalcBallAngleReflect(Collision2D collision)
    {
        float _playerPixels = 120f;
        float _unityScaleHalfPlayerPixels = _playerPixels / 2f / 100f;
        float _scaleFactor180Range = 180f/120f;

        float _angleDegUnityScale = (collision.transform.position.x - 
                                     transform.position.x + 
                                     _unityScaleHalfPlayerPixels) * 
                                     _scaleFactor180Range; 

        float _angleDeg = _angleDegUnityScale * 100f;
        float _angleRad = _angleDeg * Mathf.PI / 180f;

        return new Vector2(Mathf.Cos(_angleRad), Mathf.Sin(_angleRad));
    }

    public float GetBallSpeed()
{
    return _ballModel.Speed;
}
}