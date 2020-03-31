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
    void Start()
    {
        hungerCounter.text = "100";
        currentHunger = 100;
        sheepMoodImage.texture = sheepMoodHappy;
    }

    // Update is called once per frame
    void Update()
    {
        currentHunger -= 1 * Time.deltaTime;
        hungerToDisplay = Mathf.RoundToInt(currentHunger);
        hungerCounter.text = hungerToDisplay.ToString();
        
        if (hungerToDisplay < 90)
        {
            sheepMoodImage.texture = sheepMoodSad;
        }

        if(hungerToDisplay < 85)
        {
            sheepMoodImage.texture = sheepMoodAngry;
        }

        if(hungerToDisplay <= 0)
        {
            hungerToDisplay = 0;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            FeedSheep();
        }
    }

    void FeedSheep()
    {
        if(currentHunger <= 85)
        {
            currentHunger = currentHunger + 15;
            Debug.Log("Yummy! Thanks Penelope!");
        }
        else
        {
            currentHunger = 100;
            Debug.Log("Wow, I'm full and can't eat that all Penelope!");
        }
        Inventory.instance.Add(milk);
    }
}
