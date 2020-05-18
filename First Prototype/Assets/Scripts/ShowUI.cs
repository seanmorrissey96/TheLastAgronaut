using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    public GameObject uiObject;
    public bool isActive;
    public Item cut;
    public bool flowerInteraction;
    public GameObject sheep;
    public GameObject GPSReminder;
    public Text GPSText;
    public bool sheepHaveBeenFed;
    public bool GoToBedPrompt;

    void Start()
    {
        uiObject.SetActive(false);
        flowerInteraction = false;
        GoToBedPrompt = false;
        GPSReminder.SetActive(false);
    }

    void Update()
    {
        if(sheep != null)
            sheepHaveBeenFed = sheep.GetComponent<Hunger>().hasBeenFed;

        if (flowerInteraction && sheepHaveBeenFed && !GoToBedPrompt)
        {
            GoToBedPrompt = true;
            GPSText.text = "Sure is getting late Penelope!\nMaybe it's time to hit the hay?";
            GPSReminder.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        //Debug.Log(hunger);
        if(player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            isActive = true;
            //StartCoroutine("WaitForSec");

            if(this.tag == "Flower")
            {
                //GPSText.text = "Sure is getting late Penelope!\nMaybe it's time to hit the hay?";
                //GPSReminder.SetActive(true);
               // StartCoroutine("WaitForSec");
                flowerInteraction = true;
                Debug.Log("Interacted with flower");
            }
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(false);
            isActive = false;
            //StartCoroutine("WaitForSec");
            if(gameObject.tag == "Flower")
            {
                Inventory.instance.Add(cut);
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        GPSReminder.SetActive(false);
        //Destroy(uiObject);
        //Destroy(gameObject);
    }
}
