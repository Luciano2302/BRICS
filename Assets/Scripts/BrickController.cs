using UnityEngine;

public class BrickController : MonoBehaviour
{
    BrickModel _brickModel;

    void Start()
    {
        _brickModel = GetComponent<BrickModel>();    
    }

    public void TakeDamage(float damage){
          _brickModel.Health = _brickModel.Health - damage;
    
        if(_brickModel.Health <= 0){
          // AVISAR GameManager que brick foi destruído
          GameManager.Instance.BrickDestroyed();
          Destroy(gameObject);
        }
    }
}