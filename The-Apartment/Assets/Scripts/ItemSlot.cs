using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public Item item;
    public Image itemImage;
    public int amount;

    public ItemSlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;

    }

    public int getItemID()
    {
        return item.itemID;
    }


}
