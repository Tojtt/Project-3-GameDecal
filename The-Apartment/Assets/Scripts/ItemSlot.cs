using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Item item;
    public GameObject itemName;
    public Image itemImage;
    public int amount;

    public int getItemID()
    {
        return item.itemID;
    }


}
