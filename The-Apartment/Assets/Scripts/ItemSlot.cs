using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Item item;
    //public String itemName;
    public Image itemImage;
    public Button button;

    void Awake()
    {
        button = this.GetComponent<Button>();
    }

    public int getItemID()
    {
        return item.itemID;
    }

    
}
