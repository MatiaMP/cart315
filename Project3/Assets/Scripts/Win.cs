using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;

        if (other.CompareTag("Player") || other.name.Contains("FirstPerson"))
        {
            hasWon = true;
            
            Invoke("RestartGame", 3f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}