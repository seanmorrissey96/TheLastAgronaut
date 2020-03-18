using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;
    public Text[] counts;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        counts = itemsParent.GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (inventory.itemAdded.name == "Milk")
            {
                if (i < 1)
                {
                    slots[i].AddItem(inventory.items[i]);
                    counts[i].text = inventory.milkDictionary.Keys.Count.ToString();
                }
            }
            else if (inventory.itemAdded.name == "Medicine")
            {
                if (i == 1)
                {
                    slots[i].AddItem(inventory.items[i]);
                    counts[i].text = inventory.medDictionary.Keys.Count.ToString();
                }
            }
            else if (inventory.itemAdded.name == "Cutting")
            {
                if (i > 1)
                {
                    slots[i].AddItem(inventory.items[i]);
                    counts[i].text = inventory.cutDictionary.Keys.Count.ToString();
                }
            }
        }
    }
}
