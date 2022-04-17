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
    //public bool collectible; //Whether or not can be added to inventory <-Repetitive, because tagged as Collectible

    InventoryManager inventory;

    void SetState(string newState)
    {
        state = newState;
    }
}