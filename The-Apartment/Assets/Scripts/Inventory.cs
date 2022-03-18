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

    public void AddItem(Item item)
    {
        invItemList.Add(item);
        onItemChange.Invoke();
    }


}
