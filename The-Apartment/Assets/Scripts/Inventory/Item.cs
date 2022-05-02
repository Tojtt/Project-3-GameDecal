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
    public bool notDroppable;

    //public bool collectible; //Whether or not can be added to inventory <-Repetitive, because tagged as Collectible

    public bool taken;
    //InventoryManager inventory;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetState(string newState)
    {
        state = newState;
    }

    void OnMouseDown()
    {
        taken = true;
    }


    public bool wasTaken()
    {
        return taken;
    }

    public void SetOrder(int i)
    {
        sr.sortingOrder = i;
    }

    public void SetLayer(string name)
    {
        sr.sortingLayerName = name;
    }
}