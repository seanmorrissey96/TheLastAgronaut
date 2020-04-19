using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [SerializeField] private ParticleSystem toxins;
    private static int dayNum;

    private void Start()
    {

    }

    void Update()
    {
        ActivateParticles();
    }

    void ActivateParticles()
    {
        dayNum = DayNightCycle.dayNumber;
        var em = toxins.emission;
        em.enabled = false;

        if (dayNum > 1)
        {
            em.enabled = true;
        }
    }
}