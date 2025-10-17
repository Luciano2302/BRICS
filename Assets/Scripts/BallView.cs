using UnityEngine;

public class BallView : MonoBehaviour
{

    private BallController _ballController;


    void Start()
    {
      _ballController = GetComponent<BallController>();  
    }
    
    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag == "Enemy"){
            BrickView _brickView = collision.gameObject.GetComponent<BrickView>();
            _brickView.PerformTakeDamage(1f);
        }


        if(collision.gameObject.tag == "Player"){
            // chamar metodo de colisão para o jogador
            _ballController.AngleChange(_ballController.CalcBallAngleReflect(collision));

        }else {
            //chamar método de colisão para todos os outros game objects
            _ballController.PerfectAngleReflect(collision);
        }
    }
}
