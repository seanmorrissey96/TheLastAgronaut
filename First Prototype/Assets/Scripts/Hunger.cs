using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    public Text hungerCounter;
    public Texture sheepMoodHappy;
    public Texture sheepMoodSad;
    public Texture sheepMoodAngry;
    public RawImage sheepMoodImage;
    public float currentHunger;
    public int hungerToDisplay;
    public Item milk;

    public GameObject penelopeDialoguePanel;
    public Text dialogue1;
    public bool hasBeenFed;

    public AudioSource feedSound;

    void Start()
    {
        hungerCounter.text = "100";
        currentHunger = 100;
        sheepMoodImage.texture = sheepMoodHappy;
        penelopeDialoguePanel.SetActive(false);
        hasBeenFed = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentHunger -= 1 * Time.deltaTime;
        hungerToDisplay = Mathf.RoundToInt(currentHunger);
        hungerCounter.text = hungerToDisplay.ToString();

        if (hungerToDisplay >= 66)
        {
            sheepMoodImage.texture = sheepMoodHappy;
        }

        if (hungerToDisplay < 66 && hungerToDisplay > 33)
        {
            sheepMoodImage.texture = sheepMoodSad;
        }

        if (hungerToDisplay <= 33)
        {
            sheepMoodImage.texture = sheepMoodAngry;
        }

        if (hungerToDisplay <= 0)
        {
            hungerToDisplay = 0;
            currentHunger = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            hasBeenFed = true;
            FeedSheep();
        }

        if (hasBeenFed)
        {
            //penelopeDialoguePanel.SetActive(true);
            //dialogue1.text = "test";
            //StartCoroutine("WaitForSec");
        }
    }

    void FeedSheep()
    {
        if (currentHunger <= 85)
        {
            currentHunger = currentHunger + 15;
            hungerToDisplay = Mathf.RoundToInt(currentHunger);
            Debug.Log("Yummy! Thanks Penelope!");
        }
        else
        {
            currentHunger = 100;
            hungerToDisplay = Mathf.RoundToInt(currentHunger);
            Debug.Log("Wow, I'm full and can't eat that all Penelope!");
            //sheepMoodImage.texture = sheepMoodHappy;
        }

        //Inventory.instance.Add(milk);
        hasBeenFed = true;
        feedSound.Play();
        //Debug.Log("Wow, I'm full and can't eat that all Penelope!");
        //StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        hasBeenFed = false;
        yield return new WaitForSeconds(6);
        penelopeDialoguePanel.SetActive(false);
    }
}
