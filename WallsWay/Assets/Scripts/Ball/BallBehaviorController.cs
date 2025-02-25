using UnityEngine;

public class BallBehaviorController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float angleCorrection = 5f; // Pequena corre��o no �ngulo
    public Vector2 initialDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newDirection = SwapMovementAngle(collision);
        BallMoviment(newDirection);
    }

    Vector2 SwapMovementAngle(Collision2D collision)
    {
        Vector2 ballDirection = rb.linearVelocity.normalized; // Dire��o atual da bola
        Vector2 normal = collision.contacts[0].normal; // Normal da colis�o
        Vector2 reflectedDirection = Vector2.Reflect(ballDirection, normal); // Reflete a dire��o

        // Verificar se a bola est� deslizando muito na parede (�ngulo muito pequeno)
        float dot = Vector2.Dot(reflectedDirection, normal);
        if (Mathf.Abs(dot) < 0.1f) // Pequeno ajuste para evitar que a bola "grude" na parede
        {
            float angle = angleCorrection * Mathf.Deg2Rad; // Converter para radianos
            reflectedDirection = new Vector2(
                reflectedDirection.x * Mathf.Cos(angle) - reflectedDirection.y * Mathf.Sin(angle),
                reflectedDirection.x * Mathf.Sin(angle) + reflectedDirection.y * Mathf.Cos(angle)
            ).normalized;
        }

        return reflectedDirection;
    }

    void BallMoviment(Vector2 dir)
    {
        rb.linearVelocity = dir * speed; // Mant�m a velocidade constante
    }

    void LaunchBall()
    {
        
        rb.linearVelocity = initialDirection.normalized * speed;
    }
}
