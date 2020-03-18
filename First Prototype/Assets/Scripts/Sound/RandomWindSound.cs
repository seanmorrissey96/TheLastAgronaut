using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWindSound : MonoBehaviour
{
    public AudioSource windSound;

    void Start()
    {
        cueAudio();
    }

    void cueAudio()
    {
        Invoke("RandomSounds", Random.Range(60, 120));
    }

    void RandomSounds()
    {
        windSound.Play();
        cueAudio();
    }
}
