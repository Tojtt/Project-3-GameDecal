using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    #region Valid_Keycodes
    public HashSet<KeyCode> inventoryKeys = new HashSet<KeyCode>()
    {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2,
        KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8,
        KeyCode.Alpha9
    };
    #endregion

    #region Variables
    // list of ItemSlots, which hold the item and the amount
    public static List<ItemSlot> itemList = new List<ItemSlot>();
    // max size of list
    public static int listSize = 3;
    // index of the item that is currently selected by the Player
    public KeyCode cur = KeyCode.Alpha0;
    #endregion

    /* Adds amount number of itemID to the list. */
    public static void AddItem(int itemID, int amount)
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
            Debug.Log("Item has been added");
        }
        else
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
    void UpdateCur(KeyCode index)
    {
        if (index >= 0 && inventoryKeys.Contains(index))
        {
            this.cur = index;
        }
    }

    /* Returns the item associated with the input ID */
    private static Item GetItem(int itemID)
    {
        //TODO Retrieve item using id
        return null;
    }

    void SelectItem()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

        }
    }
}
