using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public static InventoryManager invM;

    // list of ItemSlots, which hold the item and the amount
    public List<GameObject> itemSlotList = new List<GameObject>();

    public List<Item> itemList = new List<Item>();

    public Dictionary<int, int> tempItemHolder = new Dictionary<int, int>();
    // max size of list
    public int listSize = 3;
    // index of the item that is currently selected by the Player
    public int cur = 0;
    #endregion

    #region Scene_Variables
    public GameObject itemHolderPrefab;

    public GameObject itemPrefab;

    public Transform grid;
    #endregion 

    public void Start()
    {
        invM = this;
    }

    private void FillList()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject holder = Instantiate(itemHolderPrefab, grid, false);
            ItemSlot holderScript = holder.GetComponent<ItemSlot>();
          
            itemSlotList.Add(holder);
        }
        Debug.Log(itemSlotList.Count);
    }

    /* Adds amount number of itemID to the list. */
    public void AddItem(int itemID, int amount)
    {

        // add item to existing stack if already exists
        /* for (int i = 0; i < itemSlotList.Count; i++)
        {
            Debug.Log(itemList.Count);
            ItemSlot curSlot = itemSlotList[i].GetComponent<ItemSlot>();
            if (curSlot.getItemID() == itemID)
            {
                itemSlotList[i].GetComponent<ItemSlot>().amount += amount;
                return;
            }

            tempItemHolder[itemID] += amount;
        } */
        if (tempItemHolder.ContainsKey(itemID))
        {
            tempItemHolder[itemID] += amount;
        }
        if (tempItemHolder.Count < listSize)
        {
            // make new object
            Item newItem = GetItem(itemID);
            GameObject slot = Instantiate(itemHolderPrefab, grid, false);
            slot.GetComponent<ItemSlot>().item = newItem;
            slot.GetComponent<ItemSlot>().amount = amount;
            itemSlotList.Add(slot);
            tempItemHolder[itemID] = amount;
            FillList();
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
        for (int i = 0; i < itemSlotList.Count; i++)
        {
            ItemSlot s = itemSlotList[i].GetComponent<ItemSlot>();
            if (s.getItemID() == itemID)
            {
                s.amount = Mathf.Max(0, s.amount - amount);
                if (s.amount == 0)
                {
                    itemSlotList.Remove(itemSlotList[i]);
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
    private Item GetItem(int itemID)
    {
        //TODO LATER Retrieve item using id
        GameObject g = Instantiate(itemPrefab);
        //temporarily setting inactive to make it not be in scene maybe refactor later
        g.SetActive(false);
        Item item = g.GetComponent<Item>();
        item.itemID = itemID;
        return item;   
    }

}
