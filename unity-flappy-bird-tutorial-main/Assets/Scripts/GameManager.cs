using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    
    [SerializeField] private Text gravityWarningText;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    // config for moving pipes and collectible
    [SerializeField] private GameObject collectible;
    private bool isMovingPipesActive = false;
    private int movingPipesEndScore = 0;
    
    public bool isMovingPipeActives => isMovingPipesActive;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
        
        if (gravityWarningText != null)
        {
            gravityWarningText.gameObject.SetActive(false);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;
    
        // to reset the gravity on restart
        player.ResetGravity();

        if (gravityWarningText != null)
        {
            gravityWarningText.gameObject.SetActive(false);
        }
        
        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        
        Debug.Log("Current Score: " + score); // <--- Add this line!

        // check if score htis a multiple of 10
        if (score > 0 && score % 10 == 0)
        {
            string warning = player.ApplyRandomGravity();
            StartCoroutine(ShowWarningRoutine(warning));
        }

        //spawna collectible at 30, 50, 70, so every 20, starting from 30 to let the player get a feel for the game first
        if (score >= 30 && (score - 30) % 20 == 0)
        {
            Debug.Log("Trying to spawn collectible!");
            SpawnCollectible();
        }

        // deactivate moving pipes
        if (isMovingPipesActive && score >= movingPipesEndScore)
        {
            isMovingPipesActive = false;
            UpdatePipesMovement(false);
        }
    }

    // triggered when player picksup collectible
    public void ActivateMovingPipes()
    {
        isMovingPipesActive = true;
        movingPipesEndScore = score + 10;
        UpdatePipesMovement(true);
    }

    //loops through active pipes and toggles their movement
    private void UpdatePipesMovement(bool active)
    {
        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        foreach (var pipe in pipes)
        {
            pipe.isMovingUpDown = active;
        }
    }
    // instantiates the collectible at a random vertical position
    private void SpawnCollectible()
    {
        if (collectible != null)
        {
            Vector3 spawnPos = new Vector3(3f, Random.Range(-1f, 1f), 0f);
            Instantiate(collectible, spawnPos, Quaternion.identity);
        }
    }

    private IEnumerator ShowWarningRoutine(string message)
    {
        if (gravityWarningText != null)
        {
            gravityWarningText.text = message;
            gravityWarningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            gravityWarningText.gameObject.SetActive(false);
        }
    }

}
