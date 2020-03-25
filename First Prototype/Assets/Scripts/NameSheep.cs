using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSheep : MonoBehaviour
{

    public string name;
    public GameObject inputField;
    public GameObject textDisplay;

    public void StoreName()
    {
        name = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Sheep Name: " + name;
    }
}
