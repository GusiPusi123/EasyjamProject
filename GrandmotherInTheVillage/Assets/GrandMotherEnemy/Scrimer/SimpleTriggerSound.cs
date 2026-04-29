using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class SimpleTriggerSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool hasPlayed = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (audioSource != null)
            {
                audioSource.Play();
                hasPlayed = true;
                
                // Удаляем AudioSource после того, как звук проиграется
                Destroy(audioSource, audioSource.clip.length);
                
                // Удаляем сам скрипт
                Destroy(this, audioSource.clip.length);
            }
        }
    }
}