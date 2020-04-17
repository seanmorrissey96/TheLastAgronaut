using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDelay : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("playAudio", delayTime);
    }

    private void playAudio()
    {
        audioSource.Play();
    }
}
