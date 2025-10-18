using UnityEngine;

public class BrickController : MonoBehaviour
{
    private BrickModel _brickModel;

    void Start()
    {
        _brickModel = GetComponent<BrickModel>();
        
        // Verifica se o brick tem health válido
        if (_brickModel != null && _brickModel.Health <= 0)
        {
            Debug.LogWarning("Brick criado com health <= 0: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    // Aplica dano ao brick
    public void TakeDamage(float damage)
    {
        if(_brickModel == null)
        {
            _brickModel = GetComponent<BrickModel>();
        }
        
        // Aplica o dano
        _brickModel.Health -= damage;
    
        // Verifica se o brick foi destruído
        if(_brickModel.Health <= 0)
        {
            DestroyBrick();
        }
    }

    // Destroi o brick
    private void DestroyBrick()
    {
        Debug.Log("Destruindo brick: " + gameObject.name);
        
        // Notifica o GameManager ANTES de destruir
        if(GameManager.Instance != null)
        {
            GameManager.Instance.BrickDestroyed();
        }
        
        // Destroi o objeto
        Destroy(gameObject);
    }
}