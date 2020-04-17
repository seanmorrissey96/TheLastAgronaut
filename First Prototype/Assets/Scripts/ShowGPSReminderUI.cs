using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGPSReminderUI : MonoBehaviour
{
    public GameObject uiObject;
    public bool isActive;
    public bool toBeShown;
    public bool sheepHaveBeenFed;
    public GameObject gps;
    public GameObject sheep;
    public Item cut;

    void Start()
    {
        uiObject.SetActive(false);
        //toBeShown = gps.GetComponent<GPSPatrol>().showReminderText;
    }

    private void Update()
    {
        toBeShown = gps.GetComponent<GPSPatrol>().showReminderText;
        sheepHaveBeenFed = sheep.GetComponent<Hunger>().hasBeenFed;
    }

    private void OnTriggerEnter(Collider player)
    {
        if(toBeShown && !sheepHaveBeenFed)
        {
            if (player.gameObject.tag == "Player")
            {
                uiObject.SetActive(true);
                isActive = true;
                //StartCoroutine("WaitForSec");
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
