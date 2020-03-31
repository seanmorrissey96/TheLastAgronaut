using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryTrigger : MonoBehaviour
{
    public GameObject uiObject1;
    public GameObject uiObject2;
    public GameObject uiObject3;
    public bool isActive;
    private int dayCount = 1;
    void Start()
    {
        uiObject1.SetActive(false);
        uiObject2.SetActive(false);
        uiObject3.SetActive(false);
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key in trigger");
                if (dayCount == 1)
                {
                    Debug.Log("Click 1");
                    uiObject1.SetActive(true);
                    isActive = true;
                    dayCount = 2;
                }
                else if (dayCount == 2 && isActive != true)
                {
                    Debug.Log("Click 2");
                    uiObject2.SetActive(true);
                    isActive = true;
                    dayCount = 3;
                }
                else if (dayCount == 3 && isActive != true)
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
            }
            else if(dayCount == 3)
            {
                uiObject2.SetActive(false);
                isActive = false;
            }
            else if((dayCount == 1) && isActive == true)
            {
                uiObject3.SetActive(false);
                isActive = false;
            }
        }
    }
}