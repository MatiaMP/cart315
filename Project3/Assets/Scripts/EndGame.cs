using System;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private AudioSource audioSource;
    public bool playOnlyOnce = true;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed && playOnlyOnce) return;
        
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Player") || other.name.Contains("FirstPerson"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}