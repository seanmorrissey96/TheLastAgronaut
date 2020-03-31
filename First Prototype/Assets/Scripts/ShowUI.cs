using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject uiObject;
    public bool isActive;
    public Item cut;

    void Start()
    {
        uiObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider player)
    {
        //Debug.Log(hunger);
        if(player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            isActive = true;
            //StartCoroutine("WaitForSec");
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
        uiObject.SetActive(false);
        //Destroy(uiObject);
        //Destroy(gameObject);
    }
}
