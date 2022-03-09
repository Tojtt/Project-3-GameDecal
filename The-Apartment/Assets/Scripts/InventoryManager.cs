using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    // list of ItemSlots, which hold the item and the amount
    public List<ItemSlot> itemList = new List<ItemSlot>();
    // max size of list
    public int listSize = 10;
    // index of the item that is currently selected by the Player
    public int cur = 0;
    #endregion

    /* Adds amount number of itemID to the list. */
    void AddItem(int itemID, int amount)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].getItemID() == itemID)
            {
                itemList[i].amount += amount;
                return;
            }
        }
        Item item = GetItem(itemID);
        ItemSlot slot = new ItemSlot(item, amount);
        if (itemList.Count < listSize)
        {
            itemList.Add(slot);
        } else
        {
            // capacity of list is exceeded
            Debug.Log("Inventory is full - cannot add.");
        }
        return;
    }

    void RemoveItem(int itemID, int amount)
    {
        // linear time for now
        for (int i = 0; i < itemList.Count; i++)
        {
            ItemSlot s = itemList[i];
            if (s.getItemID() == itemID)
            {
                s.amount = Mathf.Max(0, s.amount - amount);
                if (s.amount == 0)
                {
                    itemList.Remove(s); 
                }
            }
        }
        Debug.Log("No matching items found in inventory.");

    }

    /* Updates cur to different index, if valid. */
    void UpdateCur(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            this.cur = index;
        }
    }

    /* Returns the item associated with the input ID */
    Item GetItem(int itemID) {
        //TODO Retrieve item using id
        return null;
    }
}
