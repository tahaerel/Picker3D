using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip collectSound;
    static AudioSource audioSource;
    void Start()
    {
        collectSound = Resources.Load<AudioClip>("Audio/Collect");

        audioSource = GetComponent<AudioSource>();
    }
    public static void playCollectSound()
    {
        audioSource.PlayOneShot(collectSound);
    }

  
}
