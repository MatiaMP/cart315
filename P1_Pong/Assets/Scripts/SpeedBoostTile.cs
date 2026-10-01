using UnityEngine;

public class SpeedBoostTile : MonoBehaviour
{
    public float speedMultiplier = 1.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();

        if (ball != null)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity *= speedMultiplier;
            }
        }
    }
}
