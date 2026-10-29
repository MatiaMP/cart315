using UnityEngine;
using System.Collections;

public class SpeedBoostTile : MonoBehaviour
{
    public float speedMultiplier = 1.3f;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }

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

            if (_spriteRenderer != null)
            {
                StartCoroutine(FlashTile());
            }
        }
    }

    private IEnumerator FlashTile()
    {
        _spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        _spriteRenderer.color = _originalColor;
    }
    }
