using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        //icon.sprite = newItem.icon;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
    }

    public void UseItem()
    {
        if (item == null) return;

        // switches to hotbar inventory 
        if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("Trying to switch");
            Inventory.instance.SwitchHotbarInv(item);
        } else
        {
            item.Use();
        }
    }

    public void DestroySlot()
    {
        Destroy(gameObject);
    }
}
