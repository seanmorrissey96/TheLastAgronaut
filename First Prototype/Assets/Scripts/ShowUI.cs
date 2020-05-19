using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowUI : MonoBehaviour
{
    public GameObject diaryUI1;
    public GameObject diaryUI2;
    public GameObject diaryUI3;
    public GameObject uiObject;
    public GameObject penelope;
    public bool isActive;
    public Item cut;
    public bool flowerInteraction;
    public GameObject sheep;
    public GameObject GPSReminder;
    public GameObject diary;
    public Text GPSText;
    public bool sheepHaveBeenFed;
    public bool GoToBedPrompt;
    public int dayCount = 0;

    private bool dialogue1;
    private bool dialogue2;
    private bool dialogue3;
    private float playerPosition;
    private string sceneName;
    private bool winState;

    public AudioSource gps;

    void Start()
    {
        uiObject.SetActive(false);
        flowerInteraction = false;
        GoToBedPrompt = false;
        GPSReminder.SetActive(false);
    } 

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        

        if(sceneName == "AnimatedSheepLevel")
        {
            winState = false;
        }
        else if(sceneName == "BattleWinState")
        {
            winState = true;
        }

        if (penelope != null)
        {
            playerPosition = penelope.transform.position.x;
        }
        

        if(diary != null && penelope != null)
        {
            dayCount = diary.GetComponent<DiaryTrigger>().dayCount;
            

            if (dayCount == 2 && !diaryUI1.activeSelf && playerPosition > 0 && !dialogue1 && !winState)
            {
                dialogue1 = true;
                GPSText.text = "Penelope! We have a problem!";
                gps.Play();
                GPSReminder.SetActive(true);
                StartCoroutine("WaitForSec");
            }
            else if(dayCount == 3  && !diaryUI2.activeSelf && !dialogue2 && !winState)
            {
                dialogue2 = true;
                GPSText.text = "Penelope! We're under attack!";
                gps.Play();
                GPSReminder.SetActive(true);
                StartCoroutine("WaitForSec");
            }
            else if (winState && !dialogue3)
            {
                Debug.Log("Winstate: " + winState);
                GPSReminder.SetActive(true);
                dialogue2 = true;
                GPSText.text = "Penelope! You won!\n That asteroid must have been carrying a virus.";
                gps.Play();

                StartCoroutine("WaitForSec");
            }
            //else if(dayCount == 2 && !diaryUI1.activeSelf && playerPosition < 0 && !dialogue2)
            //{
            //    dialogue2 = true;
            //    GPSText.text = "Second Line";
            //    GPSReminder.SetActive(true);
            //    StartCoroutine("WaitForSec");
            //}
        }
           

        if(sheep != null)
            sheepHaveBeenFed = sheep.GetComponent<Hunger>().hasBeenFed;

        if (flowerInteraction && sheepHaveBeenFed && !GoToBedPrompt)
        {
            GoToBedPrompt = true;
            GPSText.text = "Sure is getting late Penelope!\nMaybe it's time to hit the hay?";
            gps.Play();

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
