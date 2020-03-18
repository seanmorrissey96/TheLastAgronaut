using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public GameObject trigger;
    public AudioSource sound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger.SetActive(true);
            sound.Stop();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger.SetActive(false);
            sound.Play();
        }
    }
}
