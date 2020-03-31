using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : MonoBehaviour
{
    //public Inventory inventory;
    public Item med;
    public Item milk;
    public Item cut;
    public Item ammo;


    public void CraftMedicine(Item med)
    {
        Debug.Log("here");
        Debug.Log("Item: " + med.name);
        if (Inventory.instance.i > 0 && Inventory.instance.k > 0)
        {
            Inventory.instance.Add(med);
            Inventory.instance.Remove(milk);
            Inventory.instance.Remove(cut);
        }
    }

    public void CraftAmmo(Item ammo)
    {
        if(Inventory.instance.k > 0)
        {
            Inventory.instance.Add(ammo);
            Inventory.instance.Remove(cut);
        }
    }
}
