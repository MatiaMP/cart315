using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        //move left with the pipes
        transform.position += Vector3.left * speed * Time.deltaTime;

        // cleam up if it goes off-screen
        if (transform.position.x < -15f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // when player touches it, pipes start moving and the collectible destroys itself
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ActivateMovingPipes();
            Destroy(gameObject);
        }
    }
}