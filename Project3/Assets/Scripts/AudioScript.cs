using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    private Light redLight;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        redLight = GetComponent<Light>();
        if (redLight != null)
        {
            redLight.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Checks if the player entered the trigger zone
        if (other.CompareTag("Player") || other.name.Contains("FirstPerson"))
        {
            if (redLight != null)
            {
                redLight.enabled = true;
            }
            
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
} 
