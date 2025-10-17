using UnityEngine;

public class BrickView : MonoBehaviour
{
    BrickController _brickController;
    
    void Start()
    {
        _brickController = GetComponent<BrickController>();
    }

public void PerformTakeDamage(float damage){
    _brickController.TakeDamage(damage);
}

}
