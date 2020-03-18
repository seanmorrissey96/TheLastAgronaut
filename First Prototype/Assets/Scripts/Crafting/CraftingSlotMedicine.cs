using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlotMedicine : MonoBehaviour
{
    public Inventory inventory;
    public Item med;
    public Item milk;
    public Item cut;


    public void CraftMedicine(Item med)
    {
        Debug.Log("here");
        Debug.Log("Item: " + med.name);
        if (inventory.i > 0 && inventory.k > 0)
        {
            inventory.Add(med);
            inventory.Remove(milk);
            inventory.Remove(cut);
        }
    }
}
