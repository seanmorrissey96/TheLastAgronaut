using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSheepSounds : MonoBehaviour
{
    public AudioSource randomSounds;
    public AudioClip[] audioSources;

    void Start()
    {
        cueAudio();
    }

    void cueAudio()
    {
        Invoke("RandomSounds", Random.Range(3, 10));
    }

    void RandomSounds()
    {
        randomSounds.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSounds.Play();
        cueAudio();
    }
}
