using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepHealth : MonoBehaviour
{
    public GameObject[] sheep;
    public float currentHealth;
    public float maxHealth = 150f;
    public Slider healthSlider;
    public Text healthCount;
    [HideInInspector] public bool isDestroyed1 = false;
    [HideInInspector] public bool isDestroyed2 = false;
    [HideInInspector] public bool isDestroyed3 = false;
    [HideInInspector] public bool isDestroyed4 = false;
    [HideInInspector] public bool isDestroyed5 = false;
    [HideInInspector] public bool isDestroyed6 = false;
    [HideInInspector] public bool areAllDead = false;
    public Text areAllDeadText;
    public AudioSource deathSound;
    private static int dayNum;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
    }

    void Update()
    {
        dayNum = DayNightCycle.dayNumber;

        if (dayNum > 1)
        {
            currentHealth -= 1 * Time.deltaTime;
            healthCount.text = currentHealth.ToString("0");
            healthSlider.value = currentHealth;
        }

        AllDead();

        if (currentHealth <= 125 && currentHealth > 124 && !isDestroyed1)
        {
            Death();
            isDestroyed1 = true;
        }

        if (currentHealth <= 100 && currentHealth > 99 && !isDestroyed2)
        {
            Death();
            isDestroyed2 = true;
        }

        if (currentHealth <= 75 && currentHealth > 74 && !isDestroyed3)
        {
            Death();
            isDestroyed3 = true;
        }

        if (currentHealth <= 50 && currentHealth > 49 && !isDestroyed4)
        {
            Death();
            isDestroyed4 = true;
        }

        if (currentHealth <= 25 && currentHealth > 24 && !isDestroyed5)
        {
            Death();
            isDestroyed5 = true;
        }

        if (currentHealth <= 0 && !isDestroyed6)
        {
            Death();
            AllDead();
            isDestroyed6 = true;
        }
    }

    private void AllDead()
    {
        if (currentHealth <= 0)
        {
            currentHealth = (currentHealth <= 0) ? 0 : maxHealth;
            areAllDead = true;
            areAllDeadText.text = "All of Your Sheep Are Dead!";
        }
    }

    private void Death()
    {
        sheep = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sheep[i] = transform.GetChild(i).gameObject;
        }

        deathSound.Play();

        int rand = Random.Range(0, transform.childCount);
        Destroy(transform.GetChild(rand).gameObject, 3);
        Debug.Log("A sheep has died!");
    }
}