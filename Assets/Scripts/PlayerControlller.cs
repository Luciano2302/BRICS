using UnityEngine;

public class PlayerControlller : MonoBehaviour
{
    private PlayerModel _playerModel;
    private Transform _transform;
    private float _leftBound;
    private float _rightBound;

    void Start()
    {
        _playerModel = GetComponent<PlayerModel>();
        _transform = GetComponent<Transform>();
        
        //USA AS PAREDES REAIS DA CENA
        FindWallBounds();
        
        Debug.Log($"Player iniciado - Speed: {_playerModel.Speed}");
        Debug.Log($"Limites calculados: [{_leftBound:F2}, {_rightBound:F2}]");
    }

    void FindWallBounds()
    {
        //BUSCA AS PAREDES PELOS NOMES EXATOS
        GameObject leftWall = GameObject.Find("WallLeft");
        GameObject rightWall = GameObject.Find("WallRight");
        
        if (leftWall != null && rightWall != null)
        {
            Debug.Log("Paredes encontradas! Calculando limites...");
            
            //CALCULA OS LIMITES BASEADOS NAS PAREDES REAIS
            float playerHalfWidth = GetComponent<Collider2D>().bounds.size.x / 2f;
            
            // Parede esquerda: limite direito da wall
            Collider2D leftWallCollider = leftWall.GetComponent<Collider2D>();
            float leftWallRightEdge = leftWall.transform.position.x + (leftWallCollider.bounds.size.x / 2f);
            _leftBound = leftWallRightEdge + playerHalfWidth;
            
            // Parede direita: limite esquerdo da wall  
            Collider2D rightWallCollider = rightWall.GetComponent<Collider2D>();
            float rightWallLeftEdge = rightWall.transform.position.x - (rightWallCollider.bounds.size.x / 2f);
            _rightBound = rightWallLeftEdge - playerHalfWidth;
            
            Debug.Log($"WallLeft pos: {leftWall.transform.position.x:F2}, rightEdge: {leftWallRightEdge:F2}");
            Debug.Log($"WallRight pos: {rightWall.transform.position.x:F2}, leftEdge: {rightWallLeftEdge:F2}");
            Debug.Log($"Player halfWidth: {playerHalfWidth:F2}");
        }
        else
        {
            Debug.LogError("Paredes não encontradas! Usando fallback.");
            CalculateScreenBoundsFallback();
        }
    }

    void CalculateScreenBoundsFallback()
    {
        // Fallback se não encontrar paredes
        Camera mainCamera = Camera.main;
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, mainCamera.nearClipPlane));
        
        float playerHalfWidth = GetComponent<Collider2D>().bounds.size.x / 2f;
        _leftBound = bottomLeft.x + playerHalfWidth;
        _rightBound = bottomRight.x - playerHalfWidth;
    }

    public void Move(float h)
    {
        if (h == 0f) return;
        
        float moveAmount = h * _playerModel.Speed * Time.deltaTime;
        float newX = _transform.position.x + moveAmount;
        
        //VERIFICAÇÃO RIGOROSA COM DEBUG
        bool hitLeftWall = false;
        bool hitRightWall = false;
        
        if (newX < _leftBound)
        {
            newX = _leftBound;
            hitLeftWall = true;
        }
        else if (newX > _rightBound)
        {
            newX = _rightBound;
            hitRightWall = true;
        }
        
        // Aplica movimento
        _transform.position = new Vector3(newX, _transform.position.y, _transform.position.z);
        
        // Debug quando encostar nas paredes
        if (hitLeftWall)
        {
            Debug.Log($"🚫 ENCOSTOU PAREDE ESQUERDA - Player X: {newX:F2}, Limite: {_leftBound:F2}");
        }
        else if (hitRightWall)
        {
            Debug.Log($"🚫 ENCOSTOU PAREDE DIREITA - Player X: {newX:F2}, Limite: {_rightBound:F2}");
        }
        
        // Debug ocasional
        if (Time.frameCount % 60 == 0 && h != 0)
        {
            Debug.Log($"Player X: {_transform.position.x:F2}, Limits: [{_leftBound:F2}, {_rightBound:F2}]");
        }
    }
}