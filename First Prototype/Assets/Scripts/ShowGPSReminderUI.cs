using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowGPSReminderUI : MonoBehaviour
{
    public GameObject uiObject;
    public bool isActive;
    public bool toBeShown;
    public bool sheepHaveBeenFed;
    public bool flowerInteraction;
    public GameObject gps;
    public GameObject sheep;
    public GameObject flower;
    public GameObject diary;
    public Item cut;
    public Text reminderText;
    private string sceneName;
    private int dayCount;

    void Start()
    {
        uiObject.SetActive(false);
        
        //reminderText.text = "First Reminder";
        //toBeShown = gps.GetComponent<GPSPatrol>().showReminderText;
    }

    private void Update()
    {
        if (diary != null)
        {
            dayCount = diary.GetComponent<DiaryTrigger>().dayCount;
            Debug.Log("Day: " + dayCount);
        }

            Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        toBeShown = gps.GetComponent<GPSPatrol>().showReminderText;
        sheepHaveBeenFed = sheep.GetComponent<Hunger>().hasBeenFed;
        flowerInteraction = flower.GetComponent<ShowUI>().flowerInteraction;
        //Debug.Log("Interacted" + flowerInteraction);
       
        //Debug.Log("Scene: " + sceneName);
    }

    private void OnTriggerEnter(Collider player)
    {
        if(toBeShown && !sheepHaveBeenFed)
        {
            if (player.gameObject.tag == "Player" && !sheepHaveBeenFed)
            {
                uiObject.SetActive(true);
                isActive = true;
                //StartCoroutine("WaitForSec");
            }
        }
        else if(toBeShown && flowerInteraction && dayCount == 1)
        {
            if (player.gameObject.tag == "Player" && flowerInteraction)
            {
                reminderText.text = "Sure is getting late Penelope.";
                uiObject.SetActive(true);
                isActive = true;
            }
        }
        else if(toBeShown && sheepHaveBeenFed && dayCount == 1)
        {
            if(player.gameObject.tag == "Player" && sheepHaveBeenFed && !flowerInteraction)
            {
                reminderText.text = "Have you checked on the flowers yet today?";
                uiObject.SetActive(true);
                isActive = true;
            }
            
        }
        else if(toBeShown && sheepHaveBeenFed && dayCount == 2)
        {
            if (player.gameObject.tag == "Player")
            {
                reminderText.text = "Last night an asteroid hit the island!\nI'll have to fix the door in time for tomorrow.";
                uiObject.SetActive(true);
                isActive = true;
            }
        }
        //Debug.Log(hunger);
        
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(false);
            isActive = false;
            //StartCoroutine("WaitForSec");
            if (gameObject.tag == "Flower")
            {
                Inventory.instance.Add(cut);
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3);
        uiObject.SetActive(false);
        //Destroy(uiObject);
        //Destroy(gameObject);
    }
}
