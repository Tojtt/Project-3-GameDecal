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
    public Dictionary<int, GameObject> itemSlotList = new Dictionary<int, GameObject>();

    public Dictionary<int, Item> itemList = new Dictionary<int, Item>(); //ItemID: Item

    //public Dictionary<int, int> tempItemHolder = new Dictionary<int, int>(); <<don't need anymore since for counts
    // max size of list
    public int maxListSize = 3;
    // index of the item that is currently selected by the Player
    //public int cur = 0;
    #endregion

    #region Selection_Variables
    public int currSelected = -1; //ItemID of currently selected item
    bool selecting = false;
    #endregion

    #region Scene_Variables
    public GameObject itemHolderPrefab;
    public GameObject itemPrefab;
    public Transform grid;
    #endregion

    #region Referenced_Variables
    DogScript dog;
    #endregion 

    #region Unity_Functions
    public void Start()
    {
        invM = this;
        dog = GameObject.Find("Door 306").GetComponent<DogScript>();
    }
    #endregion

    #region Inventory_Functions

    /* Adds amount number of itemID to the list. */
    public void AddItem(Item newItem)
    {
        int itemID = newItem.itemID;
        if (itemList.ContainsKey(itemID))
        {
            Debug.Log("Item already in inventory.");
        }
        if (itemList.Count < maxListSize)
        {
            itemList[itemID] = newItem;

            //Creating new ItemSlot
            GameObject slotObject = Instantiate(itemHolderPrefab, grid, false);
            ItemSlot itemSlot = slotObject.GetComponent<ItemSlot>();
            itemSlot.item = newItem;
            

            itemSlotList[itemID] = slotObject;

            Debug.Log("Item has been added");

            //Add listener for select itemSlot
            itemSlot.button.onClick.AddListener(() => SelectItem(itemID));
        }
        else
        {
            // capacity of list is exceeded
            Debug.Log("Inventory is full - cannot add.");
        }
        PrintInventory();
        return;
    }

    void RemoveItem(int itemID)
    {
        itemList.Remove(itemID);
        GameObject itemSlot = itemSlotList[itemID];
        Destroy(itemSlot);
        itemSlotList.Remove(itemID);
        Debug.Log("Removed item");
        PrintInventory();
    }

    public void SelectItem(int itemID)
    {
        currSelected = itemID;
        Debug.Log("selected" + currSelected);
        selecting = true;
        Debug.Log("Selected item");

        //Update for outside classes
        if(itemID == 10) //Check if food
        {
            Debug.Log("Yum");
            dog.holdingFood = true;
        }
    }

    public void DeselectItem()
    {
        if (selecting)
        {
            currSelected = -1;
            selecting = false;
            Debug.Log("Deselected item");

            //Update for outside classes
            Debug.Log("No food");
            dog.holdingFood = false;
        }
    }

    public Item GetSelectedItem()
    {
        if (!selecting)
        {
            Debug.Log("No currently select item.");
            return null;
        }
        return itemList[currSelected];
    }

    public int GetSelectedID()
    {
        return currSelected;
    }

    public bool HasItem(int itemID)
    {
        return itemList.ContainsKey(itemID);
    }

    public void PrintInventory()
    {
        Debug.Log("Inventory contents:");
        foreach (int itemID in itemList.Keys)
        {
            Item item = itemList[itemID];
            Debug.Log(itemID + ": " + item.itemName);
        }
        Debug.Log(".");
    }
    #endregion

    #region Outside_helpers
    
    #endregion
}
