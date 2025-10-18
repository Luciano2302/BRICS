using UnityEngine;

public class BrickController : MonoBehaviour
{
    private BrickModel _brickModel;

    void Start()
    {
        _brickModel = GetComponent<BrickModel>();
    }

    public void TakeDamage(float damage)
    {
        if(_brickModel == null)
        {
            _brickModel = GetComponent<BrickModel>();
        }
        
        _brickModel.Health -= damage;
    
        if(_brickModel.Health <= 0)
        {
            DestroyBrick();
        }
    }

   private void DestroyBrick()
{
    Debug.Log("Destruindo brick: " + gameObject.name);
    
    // ⭐ AVISA GameManager PRIMEIRO
    if(GameManager.Instance != null)
    {
        GameManager.Instance.BrickDestroyed();
    }
    
    // ⭐ DEPOIS destrói
    Destroy(gameObject);
}
}