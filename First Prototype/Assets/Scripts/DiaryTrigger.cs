using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryTrigger : MonoBehaviour
{
    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject uiObject3;

    public GameObject barnDay1;
    public GameObject barnDay2;
    public GameObject barnDoor;

    public GameObject plantDay1;
    public GameObject plantDay2;

    public GameObject treeDay1;
    public GameObject treeDay2;

    public GameObject enemyPlant;

    public AudioSource asteroidSound;

    public bool isActive;
    public int dayCount = 1;

    public DayNightCycle dnc;
    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        uiObject3.SetActive(false);
        enemyPlant.SetActive(false);
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key in trigger");
                if (dayCount == 1 && dnc.pause == true)
                {
                    Debug.Log("Click 1");
                    uiObject1.SetActive(true);
                    isActive = true;
                    asteroidSound.Play();
                    dayCount = 2;
                }
                else if (dayCount == 2 && isActive != true && dnc.pause == true)
                {
                    Debug.Log("Click 2");
                    uiObject2.SetActive(true);
                    isActive = true;
                    dayCount = 3;
                }
                else if (dayCount == 3 && isActive != true && dnc.pause == true)
                {
                    Debug.Log("Click 3");
                    uiObject3.SetActive(true);
                    isActive = true;
                    dayCount = 1;
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            if(dayCount == 2)
            {
                uiObject1.SetActive(false);
                isActive = false;
                dnc.pause = false;
                dnc.timeOfDay = 1;

                barnDay1.SetActive(false);
                barnDay2.SetActive(true);
                barnDoor.SetActive(true);

                treeDay1.SetActive(false);
                treeDay2.SetActive(true);

                plantDay1.SetActive(false);
                plantDay2.SetActive(true);
            }
            else if(dayCount == 3)
            {
                uiObject2.SetActive(false);
                isActive = false;

                dnc.pause = false;
                dnc.timeOfDay = 1;

                enemyPlant.SetActive(true);
            }
            else if((dayCount == 1) && isActive == true)
            {
                uiObject3.SetActive(false);
                isActive = false;

                dnc.pause = false;
                dnc.timeOfDay = 1;
            }
        }
    }
}