using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region singleton
    public static Inventory instance;

    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public delegate void OnItemChange();
    // called when an item function is called 
    public OnItemChange onItemChange = delegate { };
    // holds all items present in inventory
    public List<Item> invItemList = new List<Item>();

    public List<Item> hotbarList = new List<Item>();
    public HotBarControl hotbarControl;

    public void SwitchHotbarInv(Item item)
    {
        // inventory to hotbar
        foreach (Item i in invItemList)
        {
            if (i.Equals(item))
            {
                if (hotbarList.Count >= hotbarControl.HotbarSlotSize)
                {
                    Debug.Log("No more slots");
                }
                else
                {
                    hotbarList.Add(item);
                    invItemList.Remove(item);
                    onItemChange.Invoke();
                }

                return;
            }
        }

        // hotbar to inventory
        foreach (Item i in hotbarList)
        {
            if (i.Equals(item))
            {
                hotbarList.Remove(item);
                invItemList.Add(item);
                onItemChange.Invoke();
                return;
            }
        }
    }

    public void AddItem(Item item)
    {
        invItemList.Add(item);
        onItemChange.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (invItemList.Contains(item))
        {
            invItemList.Remove(item);
        } else if (hotbarList.Contains(item))
        {
            hotbarList.Remove(item);
        }
        onItemChange.Invoke();
    }

}
