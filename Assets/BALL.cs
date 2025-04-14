using UnityEngine;

public class BALL : MonoBehaviour
{
    public int ballID; // ID cua bong 1 - 6
    public float moveSpeed = 2f; // Toc do di chuyen (co the chinh trong Inspector)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tat gravity de bong khong roi
        if (rb != null)
        {
            rb.gravityScale = 0;

            // Di chuyen theo 1 trong 8 huong ngau nhien
            Vector2 direction = GetRandomDirection();
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    Vector2 GetRandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            (Vector2.up + Vector2.left).normalized,
            (Vector2.up + Vector2.right).normalized,
            (Vector2.down + Vector2.left).normalized,
            (Vector2.down + Vector2.right).normalized
        };

        return directions[Random.Range(0, directions.Length)];
    }
}