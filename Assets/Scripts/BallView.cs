using UnityEngine;

public class BallView : MonoBehaviour
{
    private BallController _ballController;
    private Rigidbody2D _rb;
    public float minSpeed = 8f;

    void Start()
    {
        _ballController = GetComponent<BallController>();
        _rb = GetComponent<Rigidbody2D>();
        
        // Debug inicial
        Debug.Log("BallView iniciado - Min Speed: " + minSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball colidiu com: " + collision.gameObject.name + " - Tag: " + collision.gameObject.tag);
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("-> Brick collision");
            _ballController.HandleBrickCollision(collision);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("-> Player collision");
            _ballController.HandlePlayerCollision(collision);
        }
        else if(collision.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("-> GameOver collision");
            _ballController.HandleGameOverCollision();
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            // PAREDES: APENAS GARANTE VELOCIDADE, SEM REFLEXÃO MANUAL
            Debug.Log("-> Wall collision - Apenas ensure speed");
            EnsureMinimumSpeed();
            
            //  DETECTA SE COLOU (velocidade zero após colisão)
            if(_rb.velocity.magnitude < 0.5f)
            {
                Debug.Log("BOLA COLOU NA PAREDE! Aplicando escape...");
                ApplyEscapeVelocity();
            }
        }
        else 
        {
            Debug.Log("-> Unknown collision: " + collision.gameObject.name);
            EnsureMinimumSpeed();
        }
    }

    public void ResetBall(Vector3 position, Vector2 direction, float speed)
    {
        transform.position = position;
        _rb.velocity = direction.normalized * speed;
        Debug.Log("Ball resetada - Velocidade: " + _rb.velocity.magnitude);
    }
    
    public void ReflectBall(Collision2D collision, Vector2 direction, float speed)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDir = Vector2.Reflect(direction.normalized, normal);
        
        // CORREÇÃO PARA ÂNGULOS PROBLEMÁTICOS
        reflectDir = CorrectProblematicAngle(reflectDir);
        
        _rb.velocity = reflectDir * speed;
        Debug.Log("Reflexão aplicada - Nova velocidade: " + _rb.velocity.magnitude);
        EnsureMinimumSpeed();
    }
    
    //  NOVO: CORRIGE ÂNGULOS QUE CAUSAM PROBLEMAS
    private Vector2 CorrectProblematicAngle(Vector2 direction)
    {
        Vector2 corrected = direction;
        
        // Evita ângulos quase horizontais
        if(Mathf.Abs(corrected.y) < 0.15f)
        {
            corrected.y = corrected.y < 0 ? -0.3f : 0.3f;
            corrected = corrected.normalized;
            Debug.Log("Ângulo corrigido: " + corrected);
        }
        
        // Evita ângulos quase verticais
        if(Mathf.Abs(corrected.x) < 0.15f)
        {
            corrected.x = corrected.x < 0 ? -0.3f : 0.3f;
            corrected = corrected.normalized;
            Debug.Log("Ângulo corrigido: " + corrected);
        }
        
        return corrected.normalized;
    }
    
    // NOVO: VELOCIDADE DE ESCAPE SE COLOU
    private void ApplyEscapeVelocity()
    {
        Vector2 escapeDir = new Vector2(
            Random.Range(-0.8f, 0.8f), 
            Random.Range(-0.8f, -0.3f) // Sempre para baixo
        ).normalized;
        
        _rb.velocity = escapeDir * minSpeed;
        Debug.Log("Velocidade de escape aplicada: " + _rb.velocity);
    }
    
    public void EnsureMinimumSpeed()
    {
        float currentSpeed = _rb.velocity.magnitude;
        
        if(currentSpeed < minSpeed)
        {
            Vector2 newVelocity = _rb.velocity.normalized * minSpeed;
            _rb.velocity = newVelocity;
            Debug.Log("Velocidade ajustada: " + currentSpeed + " -> " + minSpeed);
        }
    }
    
    void Update()
    {
        EnsureMinimumSpeed();
        
        //  DEBUG: Monitora velocidade em tempo real
        if(Time.frameCount % 60 == 0) // A cada segundo aproximadamente
        {
            Debug.Log("Velocidade atual: " + _rb.velocity.magnitude);
        }
    }
}