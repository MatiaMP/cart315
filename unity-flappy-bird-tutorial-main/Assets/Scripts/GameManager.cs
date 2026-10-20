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

        // check if score htis a multiple of 10
        if (score > 0 && score % 10 == 0)
        {
            string warning = player.ApplyRandomGravity();
            StartCoroutine(ShowWarningRoutine(warning));
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
