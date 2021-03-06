﻿using System.Collections;
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

    public GameObject gameOverScreen;
    public GameObject progressText;

    public AudioSource writing;
    public AudioSource asteroidSound;

    bool cue;

    public bool isActive;
    public int dayCount = 1;

    public DayNightCycle dnc;
    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        uiObject3.SetActive(false);
        enemyPlant.SetActive(false);
        gameOverScreen.SetActive(false);
        progressText.SetActive(false);
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            progressText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                progressText.SetActive(false);
                if (dayCount == 1 && dnc.pause == true)
                {
                    Debug.Log("Click 1");
                    uiObject1.SetActive(true);
                    isActive = true;
                    writing.Play();

                    StartCoroutine(WaitForSec());
                    dayCount = 2;
                }
                else if (dayCount == 2 && isActive != true && dnc.pause == true)
                {
                    Debug.Log("Click 2");
                    uiObject2.SetActive(true);
                    isActive = true;
                    writing.Play();
                    dayCount = 3;
                }
                else if (dayCount == 3 && isActive != true && dnc.pause == true)
                {
                    Debug.Log("Click 3");
                    uiObject3.SetActive(true);
                    isActive = true;
                    writing.Play();
                    dayCount = 1;
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            progressText.SetActive(false);
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

                writing.Stop();
            }
            else if(dayCount == 3)
            {
                uiObject2.SetActive(false);
                isActive = false;

                dnc.pause = false;
                dnc.timeOfDay = 1;

                enemyPlant.SetActive(true);

                writing.Stop();
            }
            else if((dayCount == 1) && isActive == true)
            {
                uiObject3.SetActive(false);
                isActive = false;
                gameOverScreen.SetActive(true);

                dnc.pause = false;
                dnc.timeOfDay = 1;
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(3.0f);
        asteroidSound.Play();
    }
}