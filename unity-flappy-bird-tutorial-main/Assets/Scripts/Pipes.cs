using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float speed = 5f;
    public float gap = 3f;

    private float leftEdge;

    // variables to control vertical pipe movement
    public bool isMovingUpDown = false;
    public float bobSpeed = 1.5f;
    public float bobHeight = 0.5f;
    private float startY;
    
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        bottom.position += Vector3.down * gap / 2;
        //starting height
        startY = transform.position.y;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        // make alternating movement smooth with sine wave
        if (isMovingUpDown)
        {
            float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobHeight; transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }

}
