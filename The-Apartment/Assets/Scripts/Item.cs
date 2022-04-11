using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int itemID;
    public string itemName;
    public string state; //Eg: For gas tank: "Full" or "Empty"
    public Sprite sprite;
    public bool collectible; //Whether or not can be added to inventory

    InventoryManager inventory;

    void SetState(string newState)
    {
        state = newState;
    }
    //    #region Unity_Functions
    //    void Awake()
    //    {
    //        if (collectible)
    //        {
    //            inventory = GameObject.FindWithTag("Inventory").GetComponent<InventoryManager>();
    //        }   
    //    }
    //    #endregion

    //    #region Interact_Functions
    //    void OnMouseDown()
    //    {
    //        if (collectible)
    //        {
    //            inventory.AddItem(thi, 1);
    //        }
    //    }
    //    #endregion
}


/* Items
 * 
 * GasTank
 * 
 * FireExtinguisher
 * 
 * 
 * 
 * 
 * 
 * 
 */