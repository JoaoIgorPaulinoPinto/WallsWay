using UnityEngine;

public class BallBehaviorController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public float angleCorrection = 5f; // Pequena correção no ângulo
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
        Vector2 ballDirection = rb.linearVelocity.normalized; // Direção atual da bola
        Vector2 normal = collision.contacts[0].normal; // Normal da colisão
        Vector2 reflectedDirection = Vector2.Reflect(ballDirection, normal); // Reflete a direção

        // Verificar se a bola está deslizando muito na parede (ângulo muito pequeno)
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
        rb.linearVelocity = dir * speed; // Mantém a velocidade constante
    }

    void LaunchBall()
    {
        
        rb.linearVelocity = initialDirection.normalized * speed;
    }
}
