using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    private float defaultGravity;
    private float defaultStrength;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // defaults
        defaultGravity = gravity;
        defaultStrength = strength;
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    public string ApplyRandomGravity()
    {
        // roll random choice from 0-1-2

        int randomChoice = Random.Range(0, 3);

        switch (randomChoice)
        {
            case 0:
                gravity = defaultGravity * 1.6f;
                strength = defaultStrength * 0.85f;
                if (spriteRenderer != null) spriteRenderer.color = new Color(1f, 0.5f, 0.5f);
                return "High Gravity!";

            case 1:
                gravity = defaultGravity * 0.75f;
                strength = defaultStrength * 0.95f;
                if (spriteRenderer != null) spriteRenderer.color = new Color(0.5f, 0.8f, 1f);
                return "Low Gravity!";


            case 2:
                gravity = defaultGravity;
                strength = defaultStrength;
                if (spriteRenderer != null) spriteRenderer.color = Color.green;
                return "Normal Gravity!";

        }

        return "Normal Gravity!";
    }

    //resets when restarting game
    public void ResetGravity()
    {
        gravity = defaultGravity;
        strength = defaultStrength;
        
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Scoring")) {
            GameManager.Instance.IncreaseScore();
        }
    }

}
