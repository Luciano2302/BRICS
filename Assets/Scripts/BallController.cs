using UnityEngine;


public class BallController : MonoBehaviour
{
    private BallModel _ballModel;
    private Rigidbody2D _ballRigidbody;

    void Start()
    {
        _ballModel     = GetComponent<BallModel>();
        _ballRigidbody = GetComponent<Rigidbody2D>(); 
        AngleChange(_ballModel.Direction);
    }

    public void AngleChange(Vector2 dir){
        _ballModel.Direction = dir;
        _ballRigidbody.linearVelocity = dir * _ballModel.Speed;
    }

    public void PerfectAngleReflect(Collision2D collision){
        AngleChange(Vector2.Reflect(_ballModel.Direction, collision.contacts[0].normal));
        
    }

            
    public Vector2 CalcBallAngleReflect(Collision2D collision){

        //Quantidade de pixels do player
        float _playerPixels = 120f;

        //Encontrando a metade do player e transformando para a escala da Unity
        float _unityScaleHalfPlayerPixels = _playerPixels / 2f / 100f;

        //Valor para transformar valores da escala 0 a 1.2  para 0 a 1.8 
        float _scaleFactor180Range = 180f/120f;

        //
        float _angleDegUnityScale = (collision.transform.position.x - 
                                     transform.position.x + 
                                     _unityScaleHalfPlayerPixels) * 
                                     _scaleFactor180Range; 

        //Transformando  o valor encontrado para Degree
        float _angleDeg = _angleDegUnityScale * 100f;

        //Converter para radianos
        float _angleRad = _angleDeg * Mathf.PI / 180f;

        return  new Vector2(Mathf.Cos(_angleRad),Mathf.Sin(_angleRad));
    }
}
