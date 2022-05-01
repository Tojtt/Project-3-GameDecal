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
    List<int> order = new List<int>();
    public int maxListSize = 3;

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

    #region UI_variables
    Canvas canvas;
    float H;
    float W;

    Vector3 spawnPosition;
    Vector3 spawnSpacing;
    #endregion

    #region Referenced_Variables
    DogScript dog;
    PlayerController player;
    #endregion 

    #region Unity_Functions
    public void Start()
    {
        invM = this;
        dog = GameObject.Find("Door 306-Dog").GetComponent<DogScript>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        canvas = FindObjectOfType<Canvas>();

        H= canvas.GetComponent<RectTransform>().rect.height;
        W = canvas.GetComponent<RectTransform>().rect.width;

        spawnPosition = new Vector3(W / 16, H / 7, 0);
        spawnSpacing = new Vector3(W / 24, 0, 0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selecting)
            {
                RemoveItem(currSelected, true);
            }
        }
    }
    #endregion

    #region Inventory_Functions
    public void RedrawItems()
    {
        for(int i = 0; i < order.Count; i++)
        {
            int itemID = order[i];
            GameObject itemSlot = itemSlotList[itemID];
            itemSlot.transform.position = spawnPosition + i * spawnSpacing;
        }
    }

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
            //GameObject slotObject = Instantiate(itemHolderPrefab, grid, false);
            Vector3 newSpawnPosition = spawnPosition + (itemList.Count - 1) * spawnSpacing;
            GameObject slotObject = Instantiate(itemHolderPrefab, newSpawnPosition, Quaternion.identity);// false);
            slotObject.transform.SetParent(grid);
            ItemSlot itemSlot = slotObject.GetComponent<ItemSlot>();
            
            itemSlot.item = newItem;
            itemSlot.SetIcon();

            itemSlotList[itemID] = slotObject;
            order.Add(itemID);

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

    public void RemoveItem(int itemID, bool fall)
    {
        //Drop back into world
        Item item = itemList[itemID];
        Vector2 dropPosition = player.GetPosition();
        float offset = 0.2f;
        if (player.GetDirection() == Vector2.left)
        {
            dropPosition.x -= offset;
        } else 
        {
            dropPosition.x += offset;
        }

        if (fall)
        {
            item.gameObject.transform.position = dropPosition; //Drop next to player
            item.gameObject.SetActive(true);
            StartCoroutine(Fall(item.gameObject));
        } else
        {
            //Nothing
        }

        //Remove
        order.Remove(itemID);
        itemList.Remove(itemID);
        GameObject itemSlot = itemSlotList[itemID];
        Destroy(itemSlot);
        itemSlotList.Remove(itemID);
        Debug.Log("Removed item");

        selecting = false;
        currSelected = -1;
        RedrawItems();
        PrintInventory();
    }

    IEnumerator Fall(GameObject itemObject)
    {
        float playerY = player.transform.position.y;
        float velocity = 0.2f;
        while (itemObject.transform.position.y > playerY - 1f)
        {
            itemObject.transform.position -= new Vector3(0, velocity, 0);
            velocity += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
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
